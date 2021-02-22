using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Health playerHealth;
    [Range(1, 30)]
    [SerializeField]
    private double GiveHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Character").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            double giveHealth = playerHealth.healthUntilMax();
            if(giveHealth > 0)
            {
                double gavedHealth = 0;
                if(giveHealth > GiveHealth)
                {
                    gavedHealth = GiveHealth;
                    playerHealth.SetHealth(GiveHealth);
                }
                else
                {
                    gavedHealth = giveHealth;
                    playerHealth.SetHealth(giveHealth);
                }
                Destroy(gameObject);
                gameObject.GetComponent<ScrollingText>().StartSentence((int)gavedHealth);

            }
        }
    }
}
