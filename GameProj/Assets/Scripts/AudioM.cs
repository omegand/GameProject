using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioM : MonoBehaviour
{
    public static float volumeSFX = 1;
    private static AudioSource AS;
    private void Awake()
    {
        AS = GetComponent<AudioSource>();
    }
    public static void PlaySound(AudioClip au)
    {
        AS.PlayOneShot(au, volumeSFX);
    }
    public static AudioSource createAS(AudioClip au, bool loop, float volume)
    {
        var newAS = GameObject.FindGameObjectWithTag("Player").AddComponent<AudioSource>();
        newAS.clip = au;
        newAS.loop = loop;
        newAS.playOnAwake = false;
        newAS.volume = volume;
        return newAS;
    }
}
