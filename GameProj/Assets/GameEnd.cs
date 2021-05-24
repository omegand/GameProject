using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0)
        {
            if (PlayerPrefs.GetInt("FinalItem") == 1)
            {
                PlayerPrefs.SetInt("FinalItem", 1);
                SceneManager.LoadScene("GameEnd");
            }
            else
                ScrollingText.StartSentence(new string[] { "I need to find the missing item to activate" }, new string[] { "NoLeave" });
        }
    }
}
