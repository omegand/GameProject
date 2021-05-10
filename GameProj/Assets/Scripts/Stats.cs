using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    public string objectname;
    public int level = 1;
    public float dmg;
    public float maxhp;
    public float currenthp;
    [HideInInspector] public bool defending = false;
    public float xp;

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            UpdateStats();
            TryLoading();
        }
    }

    private void TryLoading()
    {
        if (PlayerPrefs.GetInt("Load") == 1)
        {
            PlayerPrefs.SetInt("Load", 0);
            transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        }
    }

    public void UpdateStats()
    {
        if (PlayerPrefs.HasKey("xp"))
        {
            xp = PlayerPrefs.GetFloat("xp");
            level = PlayerPrefs.GetInt("lvl");
        }
        dmg = 10 * Mathf.Pow(1.1f, level);
        maxhp = 100 * Mathf.Pow(1.1f, level);
        currenthp = maxhp;
    }

    public bool Damage(float dmg)
    {
        currenthp -= dmg;
        if (currenthp <= 0) return true;
        else return false;
    }
    public void Heal(int value)
    {
        if (value > maxhp - currenthp)
        {
            currenthp = maxhp;
        }
        else currenthp += value;
    }
    public void GainXp(float amount)
    {
        xp += amount;
        if (xp >= 100)
        {
            xp = 0;
            level++;
            ScrollingText.StartSentence(new string[] { "You leveled up!" }, new string[] { "Main" });
            AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/level"), false);
        }
    }
}
