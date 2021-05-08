﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGame : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inMenu = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(inMenu)
            {
                transform.Find("MenuGame").gameObject.SetActive(false);
                inMenu = false;
                Time.timeScale = 1;
            }
            else
            {
                OpenMenu();
                inMenu = true;
                Time.timeScale = 0;
            }
        }
    }
    private void OpenMenu()
    {
        transform.Find("MenuGame").gameObject.SetActive(true);
    }
}
