using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    CharacterController cont;
    public float speed = 5f;
    float horizontal;
    float vertical;
    void Start()
    {
        cont = gameObject.GetComponent<CharacterController>();
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        float angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        cont.Move(movement * speed * Time.deltaTime);

    }
}
