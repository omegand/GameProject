using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSwitcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (!item.CompareTag("Player") && !item.CompareTag("Enemy"))
                    item.SetActive(false);
            }
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);


        }
    }


}
