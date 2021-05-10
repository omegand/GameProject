using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dynamic_Spawn : MonoBehaviour
{
    [SerializeField]
    private int MaxEnemiesPerScene;
    [SerializeField]
    private float FirstSpawnIn;
    [SerializeField]
    [Range(50, 126)]
    private int DelaySpawnS;
    [SerializeField]
    private GameObject enemy;

    private GameObject[] spawns;

    private static int SpawnCount = 0;


    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Dynamic_Spawn");
        StartCoroutine("Spawn_Enemy");
    }

    IEnumerator Spawn_Enemy()
    {
        yield return new WaitForSeconds(FirstSpawnIn);
        if(spawns.Length != 0 && SpawnCount != MaxEnemiesPerScene)
        {
            GameObject spawn = spawns[UnityEngine.Random.Range(0, spawns.Length - 1)];
            GameObject enm = Instantiate(enemy);
            enm.tag = "Dynamic_Enemy";
            BoxCollider box = spawn.GetComponentInChildren<BoxCollider>();
            //enm.transform.position = spawn.transform.position + box.center + new Vector3(UnityEngine.Random.Range(0, box.size.x / 2), UnityEngine.Random.Range(0, box.size.y / 2), UnityEngine.Random.Range(0, box.size.z));
            enm.transform.position = box.transform.position;
            SpawnCount += 1;
        }
    }
}
