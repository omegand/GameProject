using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Timed : MonoBehaviour
{
    public float time = 3f;
    void Start()
    {
        StartCoroutine(Byebye());
    }
    IEnumerator Byebye()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
