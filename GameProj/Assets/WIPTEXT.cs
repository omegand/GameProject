using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIPTEXT : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        ScrollingText.StartSentence(new string[] { "You reached the end... Work in progress." }, new string[] { "Main" });
    }
}
