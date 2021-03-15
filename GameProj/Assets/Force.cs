using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    private bool shouldForce = false;
    float mass = 1.0F;
    Vector3 impact = Vector3.zero;
    private CharacterController character;
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(impact.magnitude);
        if (impact.magnitude > 0.2F)
        {
            character.Move(impact * Time.deltaTime);
            Debug.Log("Y" + impact.y);
            impact = Vector3.Lerp(impact, Vector3.zero, 1f * Time.deltaTime);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       if(hit.collider.tag.Equals("Jump_Pad"))
       {
            if(shouldForce)
            {
                Vector3 position = hit.controller.transform.position;
                Vector3 positionTo = position;
                positionTo.y += 30;
                Vector3 dir = positionTo - position;
                AddImpact(dir, 50.0f);
                shouldForce = false;
                Debug.Log("Push");
            }
       }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("J_Activate"))
        {
            if(other.isTrigger)
            {
                if(impact.magnitude < 0.2f)
                shouldForce = true;
            }
        }
        else if(other.tag.Equals("J_Deactivate"))
        {
            shouldForce = false;
        }
    }
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y;
        impact += dir.normalized * force / mass;
    }
}
