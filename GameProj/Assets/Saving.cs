using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    Stats st;
    private void Start()
    {
        st = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }
    public void Save()
    {
        PlayerPrefs.SetInt("stage", SceneManager.GetActiveScene().buildIndex);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("xp", st.xp);
        PlayerPrefs.SetInt("lvl", st.level);
        PlayerPrefs.SetFloat("PlayerX", st.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", st.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", st.transform.position.z);
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("xp"))
        {
            Debug.Log("test");
            st.xp = PlayerPrefs.GetFloat("xp");
            st.level = PlayerPrefs.GetInt("lvl");
            st.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
            st.UpdateStats();
        }
    }
    public void GainXp(float amount)
    {
        st.xp += amount;
        if (st.xp > 100) { st.xp = 0; st.level++; }
    }
}
