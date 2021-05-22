using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTouch : MonoBehaviour
{
    [SerializeField]
    private GameObject smartEnemy;

    private bool touchedChest = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int number = Random.Range(0, 2);
            if(number <= 1)
            {
            Transform trans = transform;
            trans.LookAt(other.transform);
            GameObject chest = Instantiate(smartEnemy, trans, true);
            chest.transform.parent = null;
            Destroy(gameObject);
            }
        }

    }
}
