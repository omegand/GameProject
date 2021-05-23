using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent enemy;
    private bool stop = false;
    private GameObject player;
    private bool seen = false;
    private Vector3 patrolPos, startPos;
    private bool patrolling = false;
    private Animator anim;
    private Light enemylight;
    [SerializeField] private float sightRange, patrolVariance;
    private float givenXP;
    private int EnemyCount;
    private void Start()
    {
        enemylight = transform.Find("Spot Light").GetComponent<Light>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
        enemy = GetComponent<NavMeshAgent>();
        enemy.updateRotation = false;
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyCount = Random.Range(1, 4);
        givenXP = Random.Range((15 * EnemyCount), (33 * EnemyCount));
    }
    private void Update()
    {
        seen = Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        if (!stop)
        {
            if (!seen && !patrolling)
            {
                StartCoroutine(Patrolling());
                patrolling = true;
                anim.SetBool("moving", false);
                enemylight.color = Color.cyan;
                enemy.speed = 3;
                if (AudioM.init.backgroundM.clip == null || AudioM.init.backgroundM.clip.name == "chase")
                    AudioM.NewMethod();
            }
            if (seen)
            {
                if (!AudioM.init.backgroundM.isPlaying || AudioM.init.backgroundM.clip.name != "chase")
                    AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/chase"), true);
                enemy.speed = 5;
                Chasing();
                anim.SetBool("moving", true);
                enemylight.color = Color.red;
            }
        }

    }
    private void LateUpdate()
    {
        if (enemy.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(enemy.velocity.normalized);
        }
    }
    IEnumerator Patrolling()
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
        yield return new WaitForSeconds(5f);
        patrolling = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !stop)
        {
            StartBattle(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !stop)
        {
            StartBattle(false);

        }
    }
    public void StartBattle(bool initiative)
    {
        AudioM.StopSound(true);
        stop = true;
        PassingValues.enemycount = EnemyCount;
        PassingValues.sceneindex = SceneManager.GetActiveScene().buildIndex;
        PassingValues.savedpos = transform.position;
        PassingValues.first = initiative;
        PassingValues.xp = givenXP;
        foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (!item.CompareTag("Player") && !item.name.Equals("Screen_Blur"))
                item.SetActive(false);
        }
        if(gameObject.tag.CompareTo("Chest") == 0)
        {
            PassingValues.xp = 1000;
            SceneManager.LoadSceneAsync("CombatChest", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadSceneAsync("Combat", LoadSceneMode.Additive);
        }
        Destroy(this.gameObject);
    }
}