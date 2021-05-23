using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] Occupied;
    public GameObject[] slots;
    void Start()
    {
        Occupied = new bool[3];
        slots = new GameObject[3];
        Transform canvas = transform.Find("MainScreenText").Find("Canvas");
        Debug.Log(slots.Length);
        for (int i = 0; i < canvas.childCount; i++)
        {
            slots[i] = canvas.GetChild(i).gameObject;
        }
    }
}
