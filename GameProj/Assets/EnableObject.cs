using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private bool active;

    private GameObject lastObject;
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
        if (!active || !HandleRoom.SpawnedEnemy)
            return;

        if((other.tag.CompareTo("Enemy") != 0 || other.tag.CompareTo("Chest") != 0))
        {
            Debug.Log("Hehehe");
            if(lastObject == other.gameObject)
            {
                Destroy(door);
                active = false;
            }
            lastObject = other.gameObject;
        }
    }
}
