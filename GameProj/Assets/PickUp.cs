using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Inventory x = other.GetComponent<Inventory>();
            for (int i = 0; i < x.slots.Length; i++)
            {
                if (x.Occupied[i] == false)
                {
                    x.Occupied[i] = true;
                    x.slots[i].transform.Find("text").GetComponent<TextMeshProUGUI>().text = gameObject.name;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
