using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleSwitcher : MonoBehaviour
{
    public int EnemyCount;
    Camera cam;
    TextMeshPro text;

    private void Start()
    {
        cam = Camera.main;
        text = transform.Find("enemyCountText").GetComponent<TextMeshPro>();
        text.text = EnemyCount.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PassingValues.enemycount = EnemyCount;
            PassingValues.sceneindex = SceneManager.GetActiveScene().buildIndex;
            PassingValues.savedpos = transform.position;
            foreach (var item in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (!item.CompareTag("Player"))
                    item.SetActive(false);
            }
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
            Destroy(this.gameObject);


        }
    }
    private void LateUpdate()
    {
        transform.LookAt(cam.transform);
        transform.rotation = Quaternion.LookRotation(cam.transform.forward);
    }


}
