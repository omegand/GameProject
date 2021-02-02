using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    CharacterController cont;
    public float speed = 5f;
    public float gravity;
    private float VVelocity;
    private float JForce = 5f;
    float horizontal;
    float vertical;
    void Start()
    {
        cont = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (cont.isGrounded)
        {
            VVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
                VVelocity = JForce;
        }
        else VVelocity -= gravity * Time.deltaTime;
        Vector3 movement = new Vector3(horizontal, VVelocity, vertical);
        float angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        cont.Move(movement * speed * Time.deltaTime);

    }
}
