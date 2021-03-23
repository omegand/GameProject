using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StaticSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    void Start()
    {
        GameObject[] spawns =  GameObject.FindGameObjectsWithTag("SpawnStatic");
        foreach(GameObject spawn in spawns)
        {
            GameObject enm = Instantiate(enemy);
            enm.transform.position = spawn.transform.position;
            enm.GetComponent<NavMeshAgent>().enabled = true;
            enm.GetComponent<EnemyBehaviour>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
