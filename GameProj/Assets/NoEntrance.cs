using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEntrance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0)
        {
            ScrollingText.StartSentence(new string[] { "Urgh... I can't leave this something...?" }, new string[] { "NoLeave" });
        }
    }
}
