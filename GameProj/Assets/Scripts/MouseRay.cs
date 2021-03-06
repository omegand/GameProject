using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200f))
            {
                if (hit.transform)
                {
                    print($"o kurva {hit.transform.gameObject.name}" );
                }
            }
        }
    }
}
