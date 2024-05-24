using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTrackAndSmoothing : MonoBehaviour
{
    public Transform cameraEndPosition;
    public Transform CameraHolder;
    public Transform CameraTransformReal;
    private Vector3 velocity = Vector3.zero;
    public float transitionTime;
    public float smoothingFactor = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(CameraTransformReal.transform.position.x, CameraTransformReal.transform.position.y, CameraTransformReal.transform.position.z);

        //CameraHolder.position = Vector3.SmoothDamp(cameraEndPosition.position, targetPosition, ref velocity, transitionTime);
        cameraEndPosition.position = Vector3.SmoothDamp(cameraEndPosition.position, targetPosition, ref velocity, transitionTime);

        CameraHolder.position = cameraEndPosition.position;
        //Quaternion targetRotation = Quaternion.Euler(0f, CameraTransformReal.parent.rotation.eulerAngles.y, 0f);
        Quaternion targetRotation = CameraTransformReal.parent.rotation;
        CameraHolder.rotation = Quaternion.Lerp(CameraHolder.rotation, targetRotation, Time.deltaTime * smoothingFactor);
    }
}
