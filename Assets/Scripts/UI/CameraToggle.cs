using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondCamera;
    private bool MainCameraEnabled = true;

    private void Start()
    {
        mainCamera.enabled = true;
        secondCamera.enabled = false;
    }

    public void CameraToggler()
    {
        if (MainCameraEnabled)
        {
            mainCamera.enabled = false;
            secondCamera.enabled = true;
        }
        else
        {
            mainCamera.enabled = true;
            secondCamera.enabled = false;
        }
        MainCameraEnabled = MainCameraEnabled ? false : true;
    }
}
