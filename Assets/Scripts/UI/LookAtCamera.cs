using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Transform cameraHolder;
    // Start is called before the first frame update
    void Start()
    {
        cameraHolder = GameObject.Find("CameraHolder").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cameraHolder);
    }
}
