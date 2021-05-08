using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGame : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inMenu = false;
    GameObject menuMain;
    void Start()
    {
        Menu_Display.ActivateMain(false);
        Menu_Display.ActivateOption(false);
        menuMain = GameObject.Find("MenuGame");
        menuMain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(inMenu)
            {
                Menu_Display.ActivateMain(false);
                Menu_Display.ActivateOption(false);
                menuMain.SetActive(false);
                inMenu = false;
                Time.timeScale = 1;
            }
            else
            {
                menuMain.SetActive(true);
                OpenMenu();
                inMenu = true;
                Time.timeScale = 0;
            }
        }
    }
    private void OpenMenu()
    {
        Menu_Display.ActivateMain(true);
    }
}
