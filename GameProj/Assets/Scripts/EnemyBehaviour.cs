using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent enemy;
    private GameObject player;
    [SerializeField] private float sightRange,patrolVariance;
    private bool seen = false;
    private Vector3 patrolPos, startPos;
    private bool patrolling = false;
    private void Start()
    {
        startPos = transform.position;
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        seen = Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        if (!seen && !patrolling) { InvokeRepeating("Patrolling", 0, 4f); patrolling = true; }
        if(seen) Chasing();
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
                transform.position.x + Random.Range(-patrolVariance, patrolVariance),
                transform.position.y, 
                transform.position.z + Random.Range(-patrolVariance, patrolVariance)
                );
        }
        enemy.SetDestination(patrolPos);
    }

    void Chasing()
    {
        enemy.SetDestination(player.transform.position);
        patrolling = false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
