using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    public void Save()
    {
        if (SceneManager.GetActiveScene().name != "Combat")
        {
            Stats st = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
            PlayerPrefs.SetInt("stage", SceneManager.GetActiveScene().buildIndex);
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetFloat("xp", st.xp);
            PlayerPrefs.SetInt("lvl", st.level);
            PlayerPrefs.SetFloat("PlayerX", st.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", st.transform.position.y);
            PlayerPrefs.SetFloat("PlayerZ", st.transform.position.z);
        }
        else Debug.Log("Parodyti error, negalima per combat");

    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("xp"))
        {
            Scene current = SceneManager.GetActiveScene();
            if (current.buildIndex != PlayerPrefs.GetInt("stage"))
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("stage"), LoadSceneMode.Single);
            }
            else
            {
                Stats st = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
                st.xp = PlayerPrefs.GetFloat("xp");
                st.level = PlayerPrefs.GetInt("lvl");
                st.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
                st.UpdateStats();
            }
        }
        else
            Debug.Log("Nera nieko nx, parodyti error");
    }

}
