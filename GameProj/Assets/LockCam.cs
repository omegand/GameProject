using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCam : MonoBehaviour
{
    public CinemachineFreeLook cameraLook;
    private void Start()
    {
        cameraLook = gameObject.GetComponent<CinemachineFreeLook>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cameraLook.m_YAxis.m_InputAxisName = "Mouse Y";
            cameraLook.m_XAxis.m_InputAxisName = "Mouse X";
        }
        if (Input.GetMouseButtonUp(1))
        {
            cameraLook.m_YAxis.m_InputAxisName = "";
            cameraLook.m_XAxis.m_InputAxisName = "";
            cameraLook.m_YAxis.m_InputAxisValue = 0;
            cameraLook.m_XAxis.m_InputAxisValue = 0;
        }
    }

    
}
