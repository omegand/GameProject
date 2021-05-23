using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Stats playerHealth;
    [Range(1, 50)]
    [SerializeField]
    private int HPHeal;
    void Start()
    {
        playerHealth = GameObject.Find("Character").GetComponent<Stats>();
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
       
    }
}
