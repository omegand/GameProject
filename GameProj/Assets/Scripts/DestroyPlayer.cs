using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
   public GameObject particles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            Instantiate(particles, transform.position, Quaternion.identity);
        }
    }
}
