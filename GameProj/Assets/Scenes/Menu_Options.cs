using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Options : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene("HubArea");
    }
    public void Options()
    {
        Menu_Display.ActivateOption(true);
        Menu_Display.ActivateMain(false);
    }
    public void BackClick()
    {
        Menu_Display.ActivateOption(false);
        Menu_Display.ActivateMain(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
