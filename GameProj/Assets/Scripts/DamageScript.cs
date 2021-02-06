using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Health health;
    void Start()
    {
       health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Equals("Enemy"))
        {
            health.health = 0;

            if(health.health <= 0)
            {
               // Image health = GameObject.FindGameObjectWithTag("Health").GetComponentInChildren<Image>();
               // health.color = new Color(114, 0, 0);
                Slider slider = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
                slider.value = 0;
            }
        }
    }
}
