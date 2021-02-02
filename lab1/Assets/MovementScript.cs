using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    CharacterController cont;
    public float speed = 5f;
    public Transform Cam;
    void Start()
    {
        cont = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (movement.magnitude > 0) 
        {
            float angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            Vector3 FMov = Quaternion.Euler(0f,angle , 0f) * Vector3.forward;
            transform.rotation = Quaternion.LookRotation(FMov);
            cont.Move(FMov * speed * Time.deltaTime);
        }
    }
}
