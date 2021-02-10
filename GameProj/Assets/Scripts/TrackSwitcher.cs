using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TrackSwitcher : MonoBehaviour
{
 private int currenttrack;
 private int newtrack;
 private Random rand = new Random();
 private CinemachineDollyCart cart;
 private CinemachineVirtualCamera vcam;



    public CinemachineSmoothPath[] tracks;
    void Awake()
    {
        currenttrack = 0;
        newtrack = 0;
        cart = gameObject.GetComponent<CinemachineDollyCart>();
        vcam = gameObject.GetComponent<CinemachineVirtualCamera>();
        Reset();
    }

    public void Reset()
    {
        StopAllCoroutines();
        cart.m_Path = tracks[0];
        cart.m_Position = 0;
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
