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
    private static Stats stats;
    void Start()
    {
        menu = GameObject.Find("DeadMenu");
        GameObject.Find("Effects").GetComponent<Volume>().profile.TryGet<UnityEngine.Rendering.HighDefinition.DepthOfField>(out blur);
        stats = gameObject.GetComponent<Stats>();
        menu.SetActive(false);
    }

    // Update is called once per frame
    public static void Dead()
    {
        menu.SetActive(true);
        blur.focusMode.value = DepthOfFieldMode.Manual;
        ScrollingText.StartSentence(new string[] {stats.level.ToString(), stats.xp.ToString()}, new string[] {"LevelMessage", "XPMessage"});
    }
}
