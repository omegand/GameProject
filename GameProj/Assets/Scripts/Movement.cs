using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private VisualEffect effect;
    public LayerMask WhatIsGround;
    public int Jumps = 2;
    public float speed = 8f;
    public float JumpForce = 4f;
    public float gravity = -35f;
    private CharacterController cont;
    private Vector3 VVel;
    private Transform Ground;
    private Transform Cam;
    private bool Grounded;
    private float GCheckRadius = 0.4f;
    private Animator anim;
    private Transform swordhitbox;
    private AudioClip jumping;
    bool attacking = false;


    private bool isSlow;
    private int CurrentJumps;


    public static Movement mov;


    void Start()
    {
        jumping = Resources.Load<AudioClip>("Sounds/jump");
        effect.Stop();
        swordhitbox = transform.Find("SwordHitbox");
        cont = GetComponent<CharacterController>();
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        Ground = GameObject.Find("GroundCheck").GetComponent<Transform>();
        mov = this;
        CurrentJumps = Jumps;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Grounded = Physics.CheckSphere(Ground.position, GCheckRadius, WhatIsGround);
        if (Grounded && VVel.y < 0)
        {
            VVel.y = -1f;
            Jumps = CurrentJumps;
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (movement.magnitude > 0 && !attacking)
        {
            float angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            Vector3 FMov = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.LookRotation(FMov * -1);

            if(isSlow)
            cont.Move(FMov * (speed / 4) * Time.deltaTime);
            else
            cont.Move(FMov * speed * Time.deltaTime);

            isSlow = false;
            anim.SetBool("run", true);
        }
        else anim.SetBool("run", false);

        if (Input.GetKeyDown(KeyCode.Space) && Jumps > 0 && !attacking)
        {
            VVel.y = Mathf.Sqrt(JumpForce * -2f * gravity);
            AudioM.PlaySound(jumping, false);
            anim.SetTrigger("jump");
            Jumps--;
        }
        if (Input.GetKey(KeyCode.Mouse0) && !attacking)
        {
            attacking = true;
            effect.Play();
            anim.SetTrigger("attack");
            StartCoroutine("AttackBox");
            Invoke("resetattbool", 0.72f);
        }

        VVel.y += gravity * Time.deltaTime;
        cont.Move(VVel * Time.deltaTime);
    }
    public void SetIdle()
    {
        anim.SetBool("run", false);
        Transform rotation = GetComponent<Transform>();
        rotation.rotation = Quaternion.Euler(0, 180, 0);
        this.enabled = false;
    }

    public void resetattbool()
    {
        attacking = !attacking;
        effect.Reinit();
        effect.Stop();
    }
    IEnumerator AttackBox()
    {
        AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/shwing"), false);
        yield return new WaitForSeconds(0.3f);
        var colliders = Physics.OverlapBox(swordhitbox.position, Vector3.one * 1.5f, Quaternion.identity, LayerMask.GetMask("Hittable"));
        foreach (var item in colliders)
        {
            if (item.CompareTag("Collectable"))
            {
                AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/woodhit"), false);
                item.GetComponent<Box_Destroyed>().DestroyBox();

            }
            else
            {
                AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/swordhit"), false);
                item.GetComponent<EnemyBehaviour>().StartBattle(true);
            }

        }
        yield return new WaitForSeconds(0.42f);
    }
    public static void TemporarySlow()
    {
        mov.isSlow = true;
    }
}
