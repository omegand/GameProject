using UnityEngine;

public class Stats : MonoBehaviour
{
    public string objectname;
    public int level;
    public float dmg;
    public float maxhp;
    public float currenthp;
    public bool defending = false;
    public float xp;

    private void Start()
    {
        UpdateStats();
        if (gameObject.CompareTag("Player") && PlayerPrefs.GetInt("Load") == 1)
        {
            PlayerPrefs.SetInt("Load", 0);
            xp = PlayerPrefs.GetFloat("xp");
            level = PlayerPrefs.GetInt("lvl");
            transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
            UpdateStats();
        }
    }
    public void UpdateStats()
    {
        dmg = 10 * Mathf.Pow(1.1f, level);
        maxhp = 100 * Mathf.Pow(1.1f, level);
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
        if (xp > 100) { xp = 0; level++; }
    }
}
