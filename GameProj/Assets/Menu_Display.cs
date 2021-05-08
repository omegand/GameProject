using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Display : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameObject main;
    private static GameObject option;
    void Start()
    {
        main = GameObject.Find("Menu");
        option = GameObject.Find("OptionMenu");

        option.SetActive(false);
    }
    public static void ActivateMain(bool activate)
    {
        main.SetActive(activate);
    }
    public static void ActivateOption(bool activate)
    {
        option.SetActive(activate);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
