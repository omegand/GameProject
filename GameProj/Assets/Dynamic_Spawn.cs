using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dynamic_Spawn : MonoBehaviour
{
    [SerializeField]
    private int MaxEnemiesPerScene;
    [SerializeField]
    private float FirsSpawnIn;
    [SerializeField]
    [Range(50, 126)]
    private int DelaySpawnS;
    [SerializeField]
    private GameObject enemy;

    private GameObject[] spawns;

    private bool Spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Dynamic_Spawn");
        StartCoroutine("Spawn_Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Spawn_Enemy()
    {
        yield return new WaitForSeconds(FirsSpawnIn);
        if(spawns.Length != 0)
        {
            GameObject spawn = spawns[UnityEngine.Random.Range(0, spawns.Length - 1)];
            GameObject enm = Instantiate(enemy);
            enm.tag = "Dynamic_Enemy";
            BoxCollider box = spawn.GetComponentInChildren<BoxCollider>();
            enm.transform.position = spawn.transform.position + box.center + new Vector3(UnityEngine.Random.Range(0, box.size.x / 2), UnityEngine.Random.Range(0, box.size.y / 2), UnityEngine.Random.Range(0, box.size.z));
            Spawned = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag.Equals("Dynamic_Enemy") && Spawned)
        {
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            collision.gameObject.GetComponent<EnemyBehaviour>().enabled = true;
            Spawned = false;
        }
    }
}
