using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSwitcher : MonoBehaviour
{
    public int EnemyCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PassingValues.enemycount = EnemyCount;
            PassingValues.sceneindex = SceneManager.GetActiveScene().buildIndex;
            foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (!item.CompareTag("Player"))
                    item.SetActive(false);
            }
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
            Destroy(this.gameObject);


        }
    }


}
