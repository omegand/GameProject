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
    static TextMeshProUGUI TextMesh;
    static Queue<string> sentences;
    static string[] UItexts;

    private static ScrollingText instance;
    private static List<TextMeshProUGUI> texts;

    private bool isActive = false;

    void Awake()
    {
        sentences = new Queue<string>();
        texts = new List<TextMeshProUGUI>();
        instance = this;
    }
    private static IEnumerator Typing(bool keep)
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
        instance.StartCoroutine(Pause(keep));
    }
    public static void Reset()
    {
        sentences.Clear();
        texts.ForEach(t => t.text = "");
        texts.Clear();
        instance.isActive = false;
    }
    static IEnumerator Pause(bool keep)
    {
        yield return new WaitForSeconds(2f);
        if(!keep)
        Reset();
    }

    public static void StartSentence(string[] values, string[] UInames, bool keep = false)
    {
        if (!instance.isActive)
        {
            Debug.Log(values[0]);
            instance.isActive = true;
            UItexts = UInames;
            List<string> list = new List<string>(values);
            list.ForEach(s => sentences.Enqueue(s));
            instance.StartCoroutine(Typing(keep));
        }
    }
}
