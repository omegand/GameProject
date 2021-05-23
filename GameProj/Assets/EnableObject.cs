using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (!active)
            return;

        if(other.tag.CompareTo("Enemy") != 0)
        {
            Destroy(door);
            active = false;
        }
    }
}
