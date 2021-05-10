using UnityEngine;
using UnityEngine.SceneManagement;


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
        Scene current = SceneManager.GetActiveScene();
        switch (current.name)
        {
            case "HubArea":
                PlaySound(Resources.Load<AudioClip>("Sounds/hub"), true);
                break;
            case "layer1":
                PlaySound(Resources.Load<AudioClip>("Sounds/layer1"), true);
                break;
            default:
                PlaySound(Resources.Load<AudioClip>("Sounds/Chilly"), true);
                break;
        }
    }
    public static void PlaySound(AudioClip au, bool background)
    {
        if (background)
        {
            init.backgroundM.clip = au;
            init.backgroundM.Play();
        }
        else
        {
            init.sfxM.clip = au;
            init.sfxM.Play();
        }

    }
    public static void StopSound(bool background)
    {
        if (background)
            init.backgroundM.Stop();
    }
}
