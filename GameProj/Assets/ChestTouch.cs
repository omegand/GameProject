using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.collider.name);
    }
}
