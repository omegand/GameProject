﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSwitcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // Scene sceneToLoad = SceneManager.("Assets/Scenes/Combat");
          //  Debug.Log(sceneToLoad.name);
            SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
            foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                item.SetActive(false);
            }
            Destroy(gameObject);
          // SceneManager.MoveGameObjectToScene(this.gameObject, 1);
           // SceneManager.MoveGameObjectToScene(other.gameObject, 1);

        }
    }


}
