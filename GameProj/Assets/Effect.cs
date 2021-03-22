using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Animator animator;
    private GameObject sword;
    void Start()
    {
        Debug.Log("What");
        animator = gameObject.GetComponent<Animator>();
        sword = GameObject.Find("Swordas");
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnAnimatorIK(int layerIndex)
    {
    }
    private void OnAnimatorMove()
    {

        Debug.Log(sword.transform.position);
    }
}
