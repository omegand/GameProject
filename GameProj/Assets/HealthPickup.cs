using System.Collections;
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
            playerHealth.Heal(HPHeal);
            string[] sakiniai = { $"Picked up {HPHeal} HP potion" , "Bandymas" };
            Text.StartSentence(sakiniai);
            Destroy(gameObject);
        }
    }
}
