using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialTP : MonoBehaviour
{
    public string teleportscene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < 3; i++)
            {
                if (other.GetComponent<Inventory>().Occupied.All(x => x))
                {
                    StartCoroutine(DoIt(other));
                }
                else
                {
                    ScrollingText.StartSentence(new string[] { "You are missing keys..." }, new string[] { "Main" });
                }
            }

        }
    }
    IEnumerator DoIt(Collider other)
    {
        AudioM.PlaySound(Resources.Load<AudioClip>("Sounds/teleport"), false);
        yield return new WaitForSeconds(0.5f);
        Stats st = other.GetComponent<Stats>();
        PlayerPrefs.SetFloat("xp", st.xp);
        PlayerPrefs.SetInt("lvl", st.level);
        SceneManager.LoadScene(teleportscene);
    }
}
