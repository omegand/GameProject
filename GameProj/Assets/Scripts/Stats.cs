using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string Namee;
    public int level;
    public int dmg;
    public float maxhp;
    public float currenthp;


    public bool Damage(float dmg) 
    {
        currenthp -= dmg;
        if (currenthp <= 0) return true;
        else return false; 
    }
}
