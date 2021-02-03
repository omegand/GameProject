using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{
    [SerializeField]
    private float TypingSpeed;
    [SerializeField]
    private bool auto;
    private bool allowed;
    private int index = 0;
    public TextMeshProUGUI TextMesh;
    public string[] sentences;
    public GameObject DialogCanvas;
    public GameObject particles;

    void Start()
    {
        DialogCanvas.SetActive(false);
    }
    private void Update()
    {

        if (auto && allowed) NextSentence();
        if (Input.GetKeyDown(KeyCode.C) && allowed) NextSentence();
    }
    IEnumerator Typing()
    {
        foreach (var item in sentences[index])
        {
            TextMesh.text += item;
            yield return new WaitForSeconds(TypingSpeed);
        }
        StartCoroutine(Pause());
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1f);
        allowed = true;
    }
    public void NextSentence()
    {
        allowed = false;
        if (index < sentences.Length - 1)
        {
            index++;
            TextMesh.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            TextMesh.text = "";
            DialogCanvas.SetActive(false);
            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("veikia");
        if (other.CompareTag("Player") && !DialogCanvas.activeSelf)
        {
            DialogCanvas.SetActive(true);
            StartCoroutine(Typing());
        }
    }
}
