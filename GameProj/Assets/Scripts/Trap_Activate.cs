using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trap_Activate : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float ChanceToActivate;
    [SerializeField]
    private GameObject enemy;
    private float onGround;
    private List<GameObject> rewards;

    [SerializeField]
    private float Force;
    private CharacterController character;

    private Vector3 impact = Vector3.zero;

    private bool Active;


    private void Awake()
    {
        Active = true;
        rewards = new List<GameObject>();
    }
    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        PassingValues.WonBattle = 0;
    }
    void Update()
    {
        if (!Active)
            return;
        if (impact.magnitude > 0.2)
        {
            character.Move(impact * Time.deltaTime);
        }
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
    private void AddImpact(Vector3 force)
    {
        Debug.Log(force);
        impact += force * force.magnitude;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Active)
            return;
        GameObject gobject = other.gameObject;
        if (other.CompareTag("Reward")) 
        {
            if (!rewards.Contains(gobject.transform.parent.gameObject))
            {
                rewards.Add(gobject.transform.parent.gameObject);
            }
        }
        if (other.CompareTag("Player")) 
        {
            if(PassingValues.WonBattle == 1)
            {
                PlayerPrefs.DeleteAll();
                gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
                PassingValues.WonBattle = 0;
                Active = false;
                return;
            }
            double GetChance = UnityEngine.Random.Range(0, 1);
            if (GetChance > ChanceToActivate)
                return;

            AddImpact(character.velocity.normalized * Force);

            gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            Suprise();
        }
        if (other.CompareTag("Enemy"))
        {
            gobject.GetComponent<BattleSwitcher>().enabled = true;
            gobject.GetComponent<NavMeshAgent>().enabled = true;
            gobject.GetComponent<EnemyBehaviour>().enabled = true;
        }
    }
    private void Suprise()
    {
        if(enemy != null)
        {
            Instantiate(enemy);
            enemy.transform.position = gameObject.transform.GetChild(1).transform.position;
        }
        else
        {
            gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
