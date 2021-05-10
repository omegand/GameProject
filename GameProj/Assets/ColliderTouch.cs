using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ColliderTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.GetComponent<NavMeshAgent>().enabled = true;
        transform.GetComponent<EnemyBehaviour>().enabled = true;
    }
}
