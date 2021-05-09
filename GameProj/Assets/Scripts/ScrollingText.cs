using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScrollingText : MonoBehaviour
{
    [SerializeField]
    float TypingSpeed;
    [SerializeField]
    bool auto;

    GameObject DialogCanvas;
    bool allowed;
    int index = 0;
    TextMeshProUGUI TextMesh;
    string[] sentences;

    void Awake()
    {
        DialogCanvas = GameObject.FindGameObjectWithTag("Screentext");
        TextMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Reset();
    }
    private void Update()
    {
        if (auto && allowed) NextSentence();
        if (Input.GetKeyDown(KeyCode.C) && allowed) NextSentence();
    }
    IEnumerator Typing()
    {
        string sentence = sentences[index];
        foreach (var item in sentence)
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
            Reset();
        }
    }

    public void StartSentence(string[] values)
    {
        Debug.Log(values[0]);
        if (!DialogCanvas.activeSelf)
        {
            sentences = values;
            DialogCanvas.SetActive(true);
            StartCoroutine(Typing());
        }
    }
    void Reset() 
    {
        TextMesh.text = "";
        index = 0;
        allowed = false;
        DialogCanvas.SetActive(false);
    }
}
