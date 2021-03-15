using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent enemy;
    private GameObject player;
    [SerializeField] private float sightRange, walkRange,patrolvariance;
    private bool seen = false;
    private Vector3 patrolPos, startPos;
    private bool walking = false;
    private void Start()
    {
        startPos = transform.position;
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        seen = Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        if (!seen && !walking) { InvokeRepeating("Patrolling", 0, 4f); walking = true; }
        else Chasing();
    }
    void Patrolling()
    {
        Vector3 dist = transform.position - startPos;
        if (dist.magnitude > 1f)
        {
            patrolPos = startPos;
        }
        else
        {
            patrolPos = new Vector3(
                transform.position.x + Random.Range(-patrolvariance, patrolvariance),
                transform.position.y, 
                transform.position.z + Random.Range(-patrolvariance, patrolvariance)
                );
        }
        enemy.SetDestination(patrolPos);
    }

    void Chasing()
    {

    }
}
