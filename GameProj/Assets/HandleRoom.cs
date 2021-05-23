using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRoom : MonoBehaviour
{
    [SerializeField]
    private float Force;
    [SerializeField]
    private SpawnChest spawn;
    [SerializeField]
    private SpawnEnemies spawnEnemies;

    private CharacterController character;


    public static bool SpawnedEnemy;

    private bool Active;
    private Vector3 impact = Vector3.zero;
    void Start()
    {
        Active = true;

        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        SpawnedEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active)
            return;

        if (impact.magnitude >= 0.2)
        {
            character.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);

            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            // Active = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Active)
            return;

        GameObject gobject = other.gameObject;

        if (other.CompareTag("Player") && Active)
        {

            Vector3 position = other.transform.position;
            AddImpact(position, Force);
            if (Rooms.RollRoom() == false)
            {
                int numb = Random.Range(0, 2);

                spawn.Chest();

                spawnEnemies.Spawn();
            }
            SpawnedEnemy = true;

        }
    }
    private void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y;
        impact += dir.normalized * force;
    }
}
