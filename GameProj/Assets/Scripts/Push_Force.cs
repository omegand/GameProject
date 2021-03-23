using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_Force : MonoBehaviour
{
    [SerializeField]
    private float Force;
    private CharacterController character;

    [SerializeField]
    private GameObject touching;

    private Vector3 impact = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (impact.magnitude > 0.2)
        {
            character.Move(impact * Time.deltaTime); // move character
        }
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
    private void AddImpact(Vector3 force)
    {
        var dir = force.normalized;
        impact += dir.normalized * force.magnitude;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == touching.gameObject.tag)
        {
            AddImpact(other.gameObject.transform.position * Force);
        }
    }
}
