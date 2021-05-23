using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRoom : MonoBehaviour
{
    [SerializeField]
    private float Force;
    [SerializeField]
    private SpawnChest spawn;

    private CharacterController character;

    private bool Active;
    private Vector3 impact = Vector3.zero;
    void Start()
    {
        Active = true;

        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active)
            return;

        if (impact.magnitude > 0.2)
        {
            character.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
            Active = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Active)
            return;

        GameObject gobject = other.gameObject;

        if (other.CompareTag("Player") && Active)
        {
            AddImpact(character.velocity.normalized * Force);

           //if(Rooms.RollRoom() == false)
          //  {
                int numb = Random.Range(0, 2);

            spawn.Chest();

           // }
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    private void AddImpact(Vector3 force)
    {
        impact += force * force.magnitude;
    }
}
