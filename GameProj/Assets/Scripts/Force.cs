using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    private bool shouldForce = false;
    private bool leaping = false;
    float mass = 1.0F;
    Vector3 impact = Vector3.zero;
    private CharacterController character;
    void Start()
    {
        shouldForce = true;
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (impact.magnitude > 0.2F && leaping)
        {
            character.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 1f * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag.Equals("Player"))
        {
            Debug.Log("Push");
            if (shouldForce)
            {
                Vector3 position = hit.transform.position;
                Vector3 positionTo = position;
                positionTo.y += 30;
                Vector3 dir = positionTo - position;
                AddImpact(dir, 80.0f);
                leaping = true;
                shouldForce = false;
            }
            else
            {  
                impact = Vector3.zero;
            }
       }
    }
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y;
        impact += dir.normalized * force / mass;
    }
}
