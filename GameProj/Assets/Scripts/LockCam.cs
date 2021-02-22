using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCam : MonoBehaviour
{
    CinemachineFreeLook cameraLook;
    private void Start()
    {
        cameraLook = gameObject.GetComponent<CinemachineFreeLook>();
        Off();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            On();
        }
        if (Input.GetMouseButtonUp(1))
        {
            Off();
        }
    }

    private void On()
    {
        cameraLook.m_YAxis.m_InputAxisName = "Mouse Y";
        cameraLook.m_XAxis.m_InputAxisName = "Mouse X";
    }

    private void Off()
    {
        cameraLook.m_YAxis.m_InputAxisName = "";
        cameraLook.m_XAxis.m_InputAxisName = "";
        cameraLook.m_YAxis.m_InputAxisValue = 0;
        cameraLook.m_XAxis.m_InputAxisValue = 0;
    }

}
