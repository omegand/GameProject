using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Stats stats;

    private Image healthImage;
    private Slider slider;
    Gradient gradient;
    void Start()
    {
        stats = GetComponent<Stats>();
        slider = GameObject.FindGameObjectWithTag("Health").GetComponent<Slider>();
        healthImage = GameObject.Find("Health_Bar").GetComponent<Image>();
        gradient = new Gradient();

        GradientColorKey[] colorKeys;
        GradientAlphaKey[] alphaKey;

        colorKeys = new GradientColorKey[2];
        colorKeys[0].color = Color.red;
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = Color.green;
        colorKeys[1].time = 1.0f;

        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 0.5f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKeys, alphaKey);

    }
    private void Update()
    {
        float nextHealth = stats.currenthp / stats.maxhp;
        Color color = gradient.Evaluate(nextHealth) * 255;
        color.r = (float)Math.Round(color.r, 0);
        color.g = (float)Math.Round(color.g, 0);
        color.b = (float)Math.Round(color.b, 0);
        color.a = (float)Math.Round(color.a, 0);
        healthImage.color = new Color32((byte)color.r, (byte)color.g, (byte)color.b, (byte)color.a);
        slider.value = stats.currenthp / stats.maxhp;
    }

}
