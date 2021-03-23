
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamic_Touch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag.Equals("Dynamic_Enemy"))
        {
            Debug.Log("Huh");
        }
    }
}
