using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class Mixer : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private UnityEngine.UI.Slider EffectSlider;

    [SerializeField]
    private UnityEngine.UI.Slider BackgroundSlider;

    private string masterVolumePar = "Mixer";
    private static string effectVolumePar = "Effect";
    private static string backgroundVolumePar = "Background";


    private float EffectVolume;
    private float BackgroundVolume;



    void Start()
    {
        EffectVolume = PlayerPrefs.GetFloat(effectVolumePar, 1f);
        BackgroundVolume = PlayerPrefs.GetFloat(backgroundVolumePar, 1f);
        if(EffectSlider != null && BackgroundSlider != null)
        {
            EffectSlider.onValueChanged.AddListener(delegate { SetEffectVol(); });
            BackgroundSlider.onValueChanged.AddListener(delegate { SetBackgroundVol(); });
            UpdateSlider();
            UpdateMixer();
        }
        else
        {
            UpdateMixer();
        }
    }
    public void UpdateMixer()
    {
        SetVol(effectVolumePar, EffectVolume);
        SetVol(backgroundVolumePar, BackgroundVolume);
    }
    public void UpdateSlider()
    {
        EffectSlider.SetValueWithoutNotify(EffectVolume);
        BackgroundSlider.SetValueWithoutNotify(BackgroundVolume);
    }
    public void SetEffectVol()
    {
        SetVol(effectVolumePar, EffectSlider.value);
        SaveVol(effectVolumePar, EffectSlider.value);

    }
    public void SetBackgroundVol()
    {
        SetVol(backgroundVolumePar, BackgroundSlider.value);
        SaveVol(backgroundVolumePar, BackgroundSlider.value);

    }
    public void SetMasterVol(float vol)
    {
        SetVol(masterVolumePar, vol);

    }
    public void SetVol(string param, float volume)
    {
        var mixerVol = volume <= 0 ? -80 : Mathf.Log(volume) * 20;
        mixer.SetFloat(param, mixerVol);
    }
    public static void SaveVol(string param, float vol)
    {
        PlayerPrefs.SetFloat(param, vol);
    }
}
