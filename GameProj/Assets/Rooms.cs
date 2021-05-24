using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public static int TotalRooms = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static bool RollRoom()
    {
        if(TotalRooms <= 1)
        {
            return true;
        }
        int number = Random.Range(0, 2);
        if (number == 1)
        {
            return true;
        }
        TotalRooms -= 1;
        return false;
    }
}
