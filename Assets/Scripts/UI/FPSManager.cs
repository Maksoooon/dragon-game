using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{

    public int vSync = 0;
    public int targetFPS = 120;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = vSync;
        Application.targetFrameRate = targetFPS;
    }

}
