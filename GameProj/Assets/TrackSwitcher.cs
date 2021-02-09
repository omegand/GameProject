using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TrackSwitcher : MonoBehaviour
{
    int currenttrack;
    int newtrack;
    Random rand = new Random();

    CinemachineDollyCart cart;
    CinemachineVirtualCamera vcam;



    public CinemachineSmoothPath[] tracks;
    void Awake()
    {
        currenttrack = 0;
        newtrack = 0;
        cart = gameObject.GetComponent<CinemachineDollyCart>();
        vcam = gameObject.GetComponent<CinemachineVirtualCamera>();
        Reset();
    }

    private void Reset()
    {
        StopAllCoroutines();
        cart.m_Path = tracks[0];
        StartCoroutine(Change());
    }
    IEnumerator Change()
    {
        yield return new WaitForSeconds(rand.Next(7, 10));
        while (newtrack == currenttrack)
        {
            newtrack = rand.Next(0, tracks.Length - 1);
        }
        cart.m_Path = tracks[newtrack];
        cart.m_Position = 0;
        currenttrack = newtrack;
        StartCoroutine(Change());
    }

    public void ChangeLookAt(Transform target)
    {
        vcam.m_LookAt = target;
    }
}
