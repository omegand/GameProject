using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
    private int lastPropIndex;

    private GameObject[] foundProps;
    private void Awake()
    {
        foundProps = GameObject.FindGameObjectsWithTag(SpawnPointTag);
        lastPropIndex = -1;
    }
    private void Start()
    {
        int propCount = props.Count;
        int failedCount = 0;
        GameObject lastSpawn = null;
        foreach(GameObject gameObject in foundProps)
        {
            int ShouldSpawn = UnityEngine.Random.Range(0, 1);
            if (ShouldSpawn > SpawnChance)
                continue;

            int objectIndex = UnityEngine.Random.Range(0, propCount - 1);
            if(lastPropIndex != -1)
            {
                if(!SamePropWithinDistance)
                {
                    while(objectIndex == lastPropIndex)
                    {
                        objectIndex = UnityEngine.Random.Range(0, propCount - 1);
                        failedCount += 1;

                        if (failedCount >= 3)
                            break;
                    }
                }
            }
            if(failedCount >= 3)
            {
                failedCount = 0;
                continue;
            }
            if(lastSpawn != null)
            {
                if (Vector3.Distance(gameObject.transform.position, lastSpawn.transform.position) <= DistanceBetweenProp)
                    continue;
            }
            GameObject item = Instantiate(props[objectIndex]);
            lastPropIndex = objectIndex;
            lastSpawn = item;
            item.transform.position = gameObject.transform.position;
            item.transform.rotation = gameObject.transform.rotation;
            failedCount = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
