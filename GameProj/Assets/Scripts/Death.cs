using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.PostProcessing;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    private static UnityEngine.Rendering.HighDefinition.DepthOfField blur;
    private static GameObject menu;
    void Start()
    {
        GameObject.Find("Effects").GetComponent<Volume>().profile.TryGet<UnityEngine.Rendering.HighDefinition.DepthOfField>(out blur);
        menu = GameObject.Find("DeadMenu");
        menu.SetActive(false);
    }

    // Update is called once per frame
    public static void Dead()
    {
        menu.SetActive(true);
        blur.focusMode.value = DepthOfFieldMode.Manual;
        ScrollingText.StartSentence(new string[] {"1000000000000000", "1000000000000000"}, new string[] {"LevelMessage", "XPMessage"});
    }
}
