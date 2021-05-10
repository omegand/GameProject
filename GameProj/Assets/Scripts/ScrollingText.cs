using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
    static Queue<string> sentences;
    static string[] UItexts;

    private static ScrollingText instance;
    private static List<TextMeshProUGUI> texts;

    void Awake()
    {
        DialogCanvas = GameObject.FindGameObjectWithTag("Screentext");
        sentences = new Queue<string>();
        texts = new List<TextMeshProUGUI>();
        instance = this;
        allowed = false;
        DialogCanvas.SetActive(false);
        //Reset();
    }
    private void Update()
    {
        if (auto && allowed) NextSentence();
        if (Input.GetKeyDown(KeyCode.C) && allowed) NextSentence();
    }
    private static IEnumerator Typing()
    {
        foreach(var UI in UItexts)
        {
            string item = sentences.Dequeue();
            TextMesh = GameObject.Find(UI).GetComponent<TextMeshProUGUI>();
            texts.Add(TextMesh);
            foreach (var sentence in item)
            {
                TextMesh.text += sentence;
                yield return new WaitForSeconds(instance.TypingSpeed);
            }
        }
        instance.StartCoroutine(Pause());
    }
    public void Reset()
    {
        sentences.Clear();
        texts.ForEach(t => t.text = "");
        texts.Clear();
    }
    static IEnumerator Pause()
    {
        yield return new WaitForSeconds(2f);
        allowed = true;
    }
    public void NextSentence()
    {
        /*
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
        */
    }

    public static void StartSentence(string[] values, string[] UInames)
    {
        if (!DialogCanvas.activeSelf)
        {
            UItexts = UInames;
            List<string> list = new List<string>(values);
            list.ForEach(s => sentences.Enqueue(s));
            DialogCanvas.SetActive(true);
            instance.StartCoroutine(Typing());
        }
    }
    /*
    void Reset() 
    {
        TextMesh.text = "";
        index = 0;
        allowed = false;
        DialogCanvas.SetActive(false);
    }
    */
}
