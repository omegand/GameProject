using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform Ground;
    public LayerMask WhatIsGround;
    public float speed = 8f;
    public Transform Cam;
    public float JumpForce = 4f;
    float gravity = -22f;
    CharacterController cont;
    Vector3 VVel;
    bool Grounded;
    private float GCheckRadius = 0.4f;

    public Vector3 SavedPosition;

    private Animator animator;


    void Start()
    {
        cont = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Grounded = Physics.CheckSphere(Ground.position, GCheckRadius, WhatIsGround);
        if (Grounded && VVel.y < 0) VVel.y = 1f;
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
        if (Input.GetKeyDown(KeyCode.Space) && Grounded) VVel.y = Mathf.Sqrt(JumpForce * -2f * gravity);
        VVel.y += gravity * Time.deltaTime;
        cont.Move(VVel * Time.deltaTime);
        if (movement.magnitude < 0) cont.velocity.Set(0, 0, 0);

    }
}
