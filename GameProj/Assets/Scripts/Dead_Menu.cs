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
            PlayerPrefs.SetInt("Load", 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("stage"), LoadSceneMode.Single);
        }
        else
        {
            ScrollingText.StartSentence(new string[] { "No saved data." }, new string[] { "Main" });
        }
    }
}
