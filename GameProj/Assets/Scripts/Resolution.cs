using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Resolution : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown dropdownMenu;

    [SerializeField]
    private UnityEngine.UI.Toggle toggle;
    UnityEngine.Resolution[] resolutions;
    private int currentRez = 0;
    private int FullScreen = 0;
    void Start()
    {
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
            toggle.isOn = true;
        else
            toggle.isOn = false;

        resolutions = Screen.resolutions;
        dropdownMenu.ClearOptions();
        List<string> options = new List<string>();
        for(int i =0; i < resolutions.Length; i++)
        {
            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                currentRez = i;
                continue;
            }
  
            options.Add(resolutions[i].width + "x" + resolutions[i].height);
        }
        options.Reverse();
        currentRez = options.Count - currentRez;
        dropdownMenu.AddOptions(options);
        dropdownMenu.value = currentRez;
        dropdownMenu.RefreshShownValue();
        dropdownMenu.onValueChanged.AddListener(delegate { ChangeRez(); });
    }
    private void ChangeRez()
    {
        UnityEngine.Resolution rez = resolutions[currentRez];
        Screen.SetResolution(rez.width, rez.height, true);
    }
    public void ToggleFullScreen(bool yes)
    {
        if(toggle.isOn)
        {
            Screen.fullScreen = true;
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            Screen.fullScreen = false;
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
    }
}
