using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_Points : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<GameObject> props;
    [SerializeField]
    private string SpawnPointTag;
    [SerializeField]
    private double DistanceBetweenProp;
    [SerializeField]
    private bool SamePropWithinDistance;
    [SerializeField]
    [Range(0, 1)]
    private double SpawnChance;

    private GameObject[] foundProps;
    private void Awake()
    {
        foundProps = GameObject.FindGameObjectsWithTag(SpawnPointTag);
    }
    private void Start()
    {
        foreach(GameObject gameObject in foundProps)
        {
            GameObject item = Instantiate(props[0]);
            item.transform.position = gameObject.transform.position;
            item.transform.rotation = gameObject.transform.rotation;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
