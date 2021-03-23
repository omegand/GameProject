using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
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
        if (collision.collider.tag.Equals("Dynamic_Enemy"))
        {
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            collision.gameObject.GetComponent<EnemyBehaviour>().enabled = true;
        }
    }
}
