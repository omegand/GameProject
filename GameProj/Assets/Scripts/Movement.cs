using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public LayerMask WhatIsGround;
    public float speed = 8f;
    public float JumpForce = 4f;
    public float gravity = -35f;
    CharacterController cont;
    Vector3 VVel;
    Transform Ground;
    Transform Cam;
    bool Grounded;
    float GCheckRadius = 0.4f;
    int Jumps = 0;
    Animator animator;


    void Start()
    {
        cont = GetComponent<CharacterController>();
        Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        Ground = GameObject.Find("GroundCheck").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Grounded = Physics.CheckSphere(Ground.position, GCheckRadius, WhatIsGround);
        if (Grounded && VVel.y < 0) { VVel.y = -1f; Jumps = 2; }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (movement.magnitude > 0)
        {
            float angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            Vector3 FMov = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.LookRotation(FMov);
            cont.Move(FMov * speed * Time.deltaTime);
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Jumps > 0)
        {
            VVel.y = Mathf.Sqrt(JumpForce * -2f * gravity);
            Jumps--;
        }

        VVel.y += gravity * Time.deltaTime;
        cont.Move(VVel * Time.deltaTime);

    }
    public void SetIdle()
    {
        animator.SetBool("walking", false);
        this.enabled = false;
    }
}
