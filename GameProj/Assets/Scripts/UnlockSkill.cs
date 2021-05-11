using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockSkill : MonoBehaviour
{
    Stats player;
    public int requiredlvl;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        if(player.level >= requiredlvl)
        GetComponent<Button>().interactable = true;
        else GetComponent<Button>().interactable = false;
    }
}
