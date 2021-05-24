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

    public static int EnemyCount;

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

            StartCoroutine(BlockDoor());
            Active = false;
        }

    }
    IEnumerator BlockDoor()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
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
                int numb = Random.Range(0, 100);
                if(numb < 20)
                {
                    spawn.Chest();
                }

                spawnEnemies.Spawn();

                EnemyCount = spawnEnemies.amount;

                Debug.Log("Yolo");
            }
            else
            {
                ScrollingText.StartSentence(new string[] { "I found the room with missing item, I need to leave now" }, new string[] { "NoLeave" });
                PlayerPrefs.SetInt("FinalItem", 1);
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
