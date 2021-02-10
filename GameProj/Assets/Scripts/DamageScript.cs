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
        if (other.tag.Equals("Player"))
        {
            health.UpdateHealth(10.0f);
        }
    }
}
