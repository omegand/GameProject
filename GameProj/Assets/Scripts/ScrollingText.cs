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

    static GameObject DialogCanvas;
    static bool allowed;
    static int index = 0;
    static TextMeshProUGUI TextMesh;
    static string[] sentences;

    private static ScrollingText instance;

    void Awake()
    {
        DialogCanvas = GameObject.FindGameObjectWithTag("Screentext");
        TextMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        instance = this;
        Reset();
    }
    private void Update()
    {
        if (auto && allowed) NextSentence();
        if (Input.GetKeyDown(KeyCode.C) && allowed) NextSentence();
    }
    private static IEnumerator Typing()
    {
        string sentence = sentences[index];
        foreach (var item in sentence)
        {
            TextMesh.text += item;
            yield return new WaitForSeconds(instance.TypingSpeed);
        }
        instance.StartCoroutine(Pause());
    }
    static IEnumerator Pause()
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

    public static void StartSentence(string[] values)
    {
        if (!DialogCanvas.activeSelf)
        {
            sentences = values;
            DialogCanvas.SetActive(true);
            instance.StartCoroutine(Typing());
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
