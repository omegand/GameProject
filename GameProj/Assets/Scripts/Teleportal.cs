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

            StartCoroutine(DoIt(other));
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
