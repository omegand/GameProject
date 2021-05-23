using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChest : MonoBehaviour
{
    [SerializeField]
    public GameObject chest;

    private static SpawnChest spawnChest;
    // Start is called before the first frame update
    void Start()
    {
        spawnChest = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Chest()
    {
        Instantiate(spawnChest.chest, gameObject.GetComponentInChildren<Transform>().position, gameObject.GetComponentInChildren<Transform>().rotation);
    }
}
