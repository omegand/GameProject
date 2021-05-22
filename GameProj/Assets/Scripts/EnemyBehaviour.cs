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
    public List<GameObject> healths;
    [SerializeField] private float sightRange, patrolVariance, givenXP;
    public int EnemyCount;
    public bool FoundHealth = false;
    [SerializeField]
    public float Health;

    public GameObject healthFound;
    private void Start()
    {
        healths = new List<GameObject>();
        GameObject[] game = GameObject.FindGameObjectsWithTag("HealthB");
        healths = new List<GameObject>(game);

        enemylight = transform.Find("Spot Light").GetComponent<Light>();
        anim = GetComponent<Animator>();
        startPos = transform.position;
        enemy = GetComponent<NavMeshAgent>();
        enemy.updateRotation = false;
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyCount = Random.Range(1, 4);
        givenXP = Random.Range((15 *EnemyCount), (33*EnemyCount));
    }
    private void Update()
    {
        seen = Physics.CheckSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        if (!stop)
        {
            if(Health < 50 && healths.Count != 0)
            {
                if(!FoundHealth)
                {
                    float dist = 99999;
                    foreach (GameObject health in healths)
                    {
                        float cur = Vector3.Distance(transform.position, health.transform.position);
                        if (cur <= dist)
                        {
                            dist = cur;
                            healthFound = health;
                        }
                    }
                    FoundHealth = true;
                }
                if(FoundHealth)
                {
                    enemy.SetDestination(healthFound.transform.position);
                }
            }
            else if(Health < 50 && !patrolling)
            {
                StartCoroutine(Patrolling());
                patrolling = true;
                anim.SetBool("moving", false);
                enemylight.color = Color.cyan;
                enemy.speed = 15;
                if (AudioM.init.backgroundM.clip == null || AudioM.init.backgroundM.clip.name == "chase")
                    AudioM.NewMethod();
            }
            else if (!seen && !patrolling)
            {
                StartCoroutine(Patrolling());
                patrolling = true;
                anim.SetBool("moving", false);
                enemylight.color = Color.cyan;
                enemy.speed = 5;
                if (AudioM.init.backgroundM.clip == null || AudioM.init.backgroundM.clip.name == "chase")
                    AudioM.NewMethod();
            }
            else if (seen && Health >= 50)
            {
                if(!AudioM.init.backgroundM.isPlaying || AudioM.init.backgroundM.clip.name != "chase")
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
        /*
        if (dist.magnitude > 1f)
        {
            patrolPos = startPos;
        }
        */
       // else
        //{
            patrolPos = new Vector3(
                transform.position.x + Random.Range(-patrolVariance, patrolVariance),
                transform.position.y,
                transform.position.z + Random.Range(-patrolVariance, patrolVariance)
                );
        //}
        enemy.SetDestination(patrolPos);
        yield return new WaitForSeconds(2f);
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
        if(collision.collider.CompareTag("Player") && !stop)
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

        if(gameObject.tag.CompareTo("Chest") != 0)
        SceneManager.LoadSceneAsync("Combat", LoadSceneMode.Additive);
        else
        {
            PassingValues.enemycount = 1;
            PassingValues.xp = 500;
            SceneManager.LoadSceneAsync("CombatChest", LoadSceneMode.Additive);
        }

        Destroy(this.gameObject);
    }
}
