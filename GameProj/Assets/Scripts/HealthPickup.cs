﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Stats playerHealth;
    [Range(1, 30)]
    [SerializeField]
    private int HPHeal;
    private ScrollingText Text;
    void Start()
    {
        playerHealth = GameObject.Find("Character").GetComponent<Stats>();
        Text = GameObject.Find("MainScreenText").GetComponent<ScrollingText>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/health"), false);
            playerHealth.Heal(HPHeal);
            string[] sakiniai = { $"Picked up {HPHeal} HP potion" };
            ScrollingText.StartSentence(sakiniai, new string[] { "Main"});
            Destroy(gameObject);
        }
        if(other.CompareTag("Enemy"))
        {
            EnemyBehaviour beh = other.gameObject.GetComponent<EnemyBehaviour>();
            beh.Health += 50;
            beh.FoundHealth = false;
            beh.healths.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
