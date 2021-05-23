using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) 
        {
            gameObject.GetComponent<Movement>().speed = 25f;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            gameObject.GetComponent<Movement>().speed = 8f;
        }
    }
}
