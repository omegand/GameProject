﻿using System.Collections;
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

    private GameObject lastObject;

    private bool Active;

    private bool Spawned;


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
        impact += force * force.magnitude;
    }
    private void OnTriggerEnter(Collider other)
    {

        GameObject gobject = other.gameObject;
        if (other.CompareTag("Player") && !Spawned && Active) 
        {
            double GetChance = UnityEngine.Random.Range(0, 1);
            if (GetChance > ChanceToActivate)
                return;

            PlayerPrefs.SetInt("InTrap", 1);

            AddImpact(character.velocity.normalized * Force);

            gameObject.transform.GetChild(0).transform.gameObject.SetActive(true);
            Suprise();
            Spawned = true;

            StartCoroutine(SetActive());

        }
        if (other.CompareTag("Enemy"))
        {
            gobject.GetComponent<NavMeshAgent>().enabled = true;
            gobject.GetComponent<EnemyBehaviour>().enabled = true;
        }
    }
    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(2f);
        Active = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (Active)
            return;

        if ((other.tag.CompareTo("Enemy") != 0 || other.tag.CompareTo("Chest") != 0))
        {
            if (lastObject == other.gameObject)
            {
                Debug.Log("Yuuuuu");
                gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
                Active = true;
            }
            lastObject = other.gameObject;
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
