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
    AudioM audiom;
    void Start()
    {
        audiom = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioM>();
        playerHealth = GameObject.Find("Character").GetComponent<Stats>();
        Text = GameObject.Find("MainScreenText").GetComponent<ScrollingText>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audiom.PlaySound(Resources.Load<AudioClip>("Sounds/health"));
            playerHealth.Heal(HPHeal);
            string[] sakiniai = { $"Picked up {HPHeal} HP potion" };
            Text.StartSentence(sakiniai);
            Destroy(gameObject);
        }
    }
}
