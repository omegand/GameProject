using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{
    [SerializeField]
    private float TypingSpeed;
    [SerializeField]
    private GameObject DialogCanvas;
    [SerializeField]
    private bool auto;
    private bool allowed;
    private int index = 0;
    TextMeshProUGUI TextMesh;
    public string[] sentences;
    private bool finished;
    void Start()
    {
        TextMesh = DialogCanvas.GetComponentInChildren<TextMeshProUGUI>();
        DialogCanvas.SetActive(false);
    }
    private void Update()
    {
        if (auto && allowed) NextSentence();
        if (Input.GetKeyDown(KeyCode.C) && allowed) NextSentence();
    }
    IEnumerator Typing(int value)
    {
        string sentence = sentences[index].Replace("{0}", value.ToString());
        Debug.Log(sentence);
        foreach (var item in sentence)
        {
            TextMesh.text += item;
            yield return new WaitForSeconds(TypingSpeed);
        }
       // StartCoroutine(Pause());
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
           // StartCoroutine(Typing());
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
            //StartCoroutine(Typing());
        }
    }
    public void StartSentence(int value)
    {
        if(!DialogCanvas.activeSelf && !finished)
        {
            DialogCanvas.SetActive(true);
            StartCoroutine(Typing(value));
        }
    }
}
