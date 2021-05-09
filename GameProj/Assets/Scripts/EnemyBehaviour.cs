using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent enemy;
    private GameObject player;
    [SerializeField] private float sightRange, patrolVariance;
    private bool seen = false;
    private Vector3 patrolPos, startPos;
    private bool patrolling = false;
    private Animator anim;
    private Light light;
    private void Start()
    {
        light = transform.Find("Spot Light").GetComponent<Light>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
        enemy = GetComponent<NavMeshAgent>();
        enemy.updateRotation = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        seen = Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        if (!seen && !patrolling)
        {
            InvokeRepeating("Patrolling", 0, 5f);
            patrolling = true;
            anim.SetBool("moving", false);
            light.color = Color.cyan;
            enemy.speed = 3;
        }
        if (seen) {
            enemy.speed = 5;
            Chasing();
            anim.SetBool("moving", true);
            light.color = Color.red;
        }
    }
    private void LateUpdate()
    {
        if (enemy.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(enemy.velocity.normalized);
        }
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }
}
