using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Resolution : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown dropdownMenu;
    // Start is called before the first frame update
    UnityEngine.Resolution[] resolutions;
    private int currentRez = 0;
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ChangeRez()
    {
        UnityEngine.Resolution rez = resolutions[currentRez];
        Screen.SetResolution(rez.width, rez.height, true);
    }
}
