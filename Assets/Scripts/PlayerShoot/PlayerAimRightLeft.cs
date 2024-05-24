using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAimRightLeft : MonoBehaviour
{
    public GameObject joystickGO;
    public TextMeshProUGUI angleText;
    public TextMeshProUGUI angleText1;
    public Transform player;
    private JoyStickSimple joystickSimple;
    public float multiplierX;
    public float multiplierY;
    public bool invertY = true;
    public float rotationAngleX;
    public float rotationAngleY;
    public float maxAngle = 30f;
    public float minAngle = 0.5f;

    public float smoothingFactor = 5f;
    private void Start()
    {
        joystickSimple = joystickGO.GetComponent<JoyStickSimple>();

    }

    private void Update()
    {
        RotatePlayerTowardsJoystick();

        // Update UI texts
        UpdateUITexts();
    }
    private void RotatePlayerTowardsJoystick()
    {
        // Calculate rotation angles
        rotationAngleX = joystickSimple.xDiff * multiplierX * -1;
        rotationAngleY = invertY ? joystickSimple.yDiff * multiplierY * -1 : joystickSimple.yDiff * multiplierY;
        rotationAngleY = Mathf.Clamp(rotationAngleY, minAngle, maxAngle);

        // Smoothly update player rotation
        Quaternion targetRotation = Quaternion.Euler(-rotationAngleY, rotationAngleX, 0);
        //player.rotation = Quaternion.Lerp(player.rotation, targetRotation, Time.deltaTime * smoothingFactor);
        player.rotation = Quaternion.Slerp(player.rotation, targetRotation, Time.deltaTime * smoothingFactor);

    }

    private void UpdateUITexts()
    {
        // Update UI texts with rotation angle information
        angleText.SetText(Convert.ToString((float)Mathf.RoundToInt(Mathf.Abs(360 - player.rotation.eulerAngles.x) / 30f * 100) + "%"));
        angleText1.SetText(Convert.ToString(Mathf.RoundToInt(Mathf.Abs(360 - player.rotation.eulerAngles.x))) + "°");
    }

    public void ToggleInvert(Toggle toggle)
    {
        if (toggle.isOn)
        {
            invertY = true;

        }
        else
        {
            invertY = false;
        }
    }
}