using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportal : MonoBehaviour
{
    public string teleportscene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Stats st = other.GetComponent<Stats>();
            PlayerPrefs.SetFloat("xp", st.xp);
            PlayerPrefs.SetInt("lvl", st.level);
            SceneManager.LoadScene(teleportscene);
        }
    }
}
