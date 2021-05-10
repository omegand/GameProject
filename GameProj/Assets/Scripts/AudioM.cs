using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioM : MonoBehaviour
{
    [SerializeField]
    public AudioSource backgroundM;
    [SerializeField]
    public AudioSource sfxM;
    public static float volumeSFX = 1;
    public static AudioM init;
    private void Awake()
    {
        init = this;
        backgroundM.loop = true;
    }
    public static void PlaySound(AudioClip au, bool background)
    {
        init.backgroundM.clip = au;
        if (background)
            init.backgroundM.Play();
        else
            init.sfxM.Play();
    }
    public static void StopSound(bool background)
    {
        if (background)
            init.backgroundM.Stop();
    }
}
