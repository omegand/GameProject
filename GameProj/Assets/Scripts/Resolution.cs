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
    List<UnityEngine.Resolution> Resolutions;
    private int currentRez = 0;
    private int FullScreen = 0;
    private void Awake()
    {
        Resolutions = new List<UnityEngine.Resolution>();
    }
    void Start()
    {
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
            toggle.isOn = true;
        else
            toggle.isOn = false;

        if(dropdownMenu != null)
        {
            resolutions = Screen.resolutions;
            Resolutions = new List<UnityEngine.Resolution>(resolutions);
            dropdownMenu.ClearOptions();
            List<string> options = new List<string>();
            Resolutions.Reverse();
            for (int i = 0; i < Resolutions.Count; i++)
            {
                if (options.Contains(Resolutions[i].width + "x" + Resolutions[i].height))
                    continue;

                if (Screen.currentResolution.width == Resolutions[i].width && Screen.currentResolution.height == Resolutions[i].height)
                {
                    currentRez = i;
                    continue;
                }

                options.Add(Resolutions[i].width + "x" + Resolutions[i].height);
            }
            dropdownMenu.AddOptions(options);
            dropdownMenu.value = currentRez;
            dropdownMenu.RefreshShownValue();
            dropdownMenu.onValueChanged.AddListener(delegate { ChangeRez(); });
        }
    }
    private void ChangeRez()
    {
        UnityEngine.Resolution rez = Resolutions[dropdownMenu.value + 1];
        Screen.SetResolution(rez.width, rez.height, toggle.isOn);
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
