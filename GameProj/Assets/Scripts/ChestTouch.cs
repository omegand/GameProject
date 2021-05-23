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
            int number = Random.Range(0, 99);
            if(number <= 20)
            {
                Transform trans = transform;
                trans.LookAt(other.transform);
                GameObject chest = Instantiate(smartEnemy, trans.position, trans.rotation);
                chest.transform.parent = null;
                Destroy(gameObject);
            }
            else
            {
                ScrollingText.StartSentence(new string[] { "No suprise this time and you receive some juicy loot" }, new string[] { "Reward" });
                other.GetComponent<Stats>().GainXp(70f);
                other.GetComponent<Stats>().currenthp += 30;
                Destroy(gameObject);
            }
        }

    }
}
