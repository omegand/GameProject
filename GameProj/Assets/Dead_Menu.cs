using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("HubArea");
    }
    public void LastSave()
    {
        Load();
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("xp"))
        {
            Scene current = SceneManager.GetActiveScene();
            if (current.buildIndex != PlayerPrefs.GetInt("stage"))
            {
                PlayerPrefs.SetInt("Load", 1);
                SceneManager.LoadScene(PlayerPrefs.GetInt("stage"), LoadSceneMode.Single);
            }
            else
            {
                Stats st = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
                st.xp = PlayerPrefs.GetFloat("xp");
                st.level = PlayerPrefs.GetInt("lvl");
                st.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
                st.UpdateStats();
                ScrollingText.StartSentence(new string[] { "Loaded progress." }, new string[] { "Main" });
            }
        }
        else
        {
            ScrollingText.StartSentence(new string[] { "No saved data." }, new string[] { "Main" });
        }
    }
}
