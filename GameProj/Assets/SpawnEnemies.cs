using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private List<Transform> enemies;

    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Transform>(gameObject.GetComponentsInChildren<Transform>());

        Debug.Log(enemies.Count);

        amount = Random.Range(0, enemies.Count) + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        Debug.Log("Hey" + amount);
        for (int i = 0; i < amount; i++)
        {
            Instantiate(enemy, enemies[i].position, enemies[i].rotation);
        }
    }


}
