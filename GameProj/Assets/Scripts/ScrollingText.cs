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
    TextMeshProUGUI TextMesh;
    public string[] sentences;
    GameObject DialogCanvas;
    private bool finished;
    void Start()
    {
        DialogCanvas = GameObject.FindGameObjectWithTag("Screentext");
        TextMesh = GameObject.FindGameObjectWithTag("Screentext").GetComponent<TextMeshProUGUI>();
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
            finished = true;
        }
        else
        {
            TextMesh.text = "";
            DialogCanvas.SetActive(false);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !DialogCanvas.activeSelf && !finished)
        {
            DialogCanvas.SetActive(true);
            StartCoroutine(Typing());
        }
    }
}
