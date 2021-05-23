using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] available;
    public List<GameObject> slots;
    // Start is called before the first frame update
    void Start()
    {
        Transform canvas = transform.Find("MainScreenText").Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            slots.Add(canvas.GetChild(i).gameObject);

        }
        foreach (var item in slots)
        {
            Debug.Log(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
