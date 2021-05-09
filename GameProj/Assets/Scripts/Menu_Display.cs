using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Display : MonoBehaviour
{
    private GameObject main;
    private GameObject option;
    private GameObject image;
    private ScrollingText text;
    private bool inMenu = false;
    private bool StartingScreen;

    void Awake()
    {
        StartingScreen = SceneManager.GetActiveScene().name == "Menu";
        Debug.Log("Dumbass");
        main = transform.Find("Menu").gameObject;
        option = transform.Find("OptionMenu").gameObject;
        image = transform.Find("Image").gameObject;
        text = GameObject.Find("MainScreenText").GetComponent<ScrollingText>();

    }
    void Update()
    {
        Debug.Log(StartingScreen);
        if (Input.GetKeyDown(KeyCode.Escape) && !StartingScreen)
        {
            if (inMenu)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    private void Open()
    {
        image.SetActive(true);
        ChangeMainState(true);
        inMenu = true;
        Time.timeScale = 0;
    }

    private void Close()
    {
        image.SetActive(false);
        ChangeMainState(false);
        ChangeOptionState(false);
        inMenu = false;
        Time.timeScale = 1;
    }

    public void ChangeMainState(bool state)
    {
        main.SetActive(state);
    }
    public void ChangeOptionState(bool state)
    {
        option.SetActive(state);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("HubArea");
    }
    public void Options()
    {
        ChangeOptionState(true);
        ChangeMainState(false);
    }
    public void BackClick()
    {
        ChangeOptionState(false);
        ChangeMainState(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

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
            text.StartSentence(new string[] { "Saved progress." });
            Close();
        }
        else
        {
            text.StartSentence(new string[] { "Cannot save in combat." });
            Close();
        }

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
                text.StartSentence(new string[] { "Loaded progress." });
                Close();
            }
        }
        else
        {
            text.StartSentence(new string[] { "No saved data." });
            Close();
        }
    }
}
