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
    public bool defending = false;


    public bool Damage(float dmg) 
    {
        Debug.Log(dmg.ToString() + " bruh");
        currenthp -= dmg;
        if (currenthp <= 0) return true;
        else return false; 
    }
    public void Heal(int value)
    {
        if (value > maxhp - currenthp)
        {
            currenthp = maxhp;
        }
        else currenthp += value;
    }
}
