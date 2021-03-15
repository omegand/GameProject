using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class fpscounter : MonoBehaviour
{
    TextMeshProUGUI counter;
    float avg = 0F;
    void Start()
    {
        counter = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("UpdateFPS", 0, 0.1f);

    }
    private void Update()
    {

    }
    void UpdateFPS() 
    {
        avg += ((Time.deltaTime / Time.timeScale) - avg) * 0.03f; 
        counter.text = $"{1f/avg:0.00} FPS";
    }


}

