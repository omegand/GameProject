using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTouch : MonoBehaviour
{
    [SerializeField]
    private GameObject smartEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int number = Random.Range(0, 99);
            if(number >= 0)
            {
                ScrollingText.StartSentence(new string[] { "It's a trap!" }, new string[] { "Main" });
                Transform trans = transform;
                trans.LookAt(other.transform);
                GameObject chest = Instantiate(smartEnemy, trans.position, trans.rotation);
                chest.transform.parent = null;
                Destroy(gameObject);
            }
            else
            {
                ScrollingText.StartSentence(new string[] { "You recieve loot and health." }, new string[] { "Main" });
                other.GetComponent<Stats>().GainXp(70f);
                other.GetComponent<Stats>().currenthp += 30;
                Destroy(gameObject);
            }
        }

    }
}
