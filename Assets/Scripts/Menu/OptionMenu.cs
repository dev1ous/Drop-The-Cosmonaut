using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Button touchButton = null;
    [SerializeField] private Button gyroButton = null;

    void Start()
    {
        if (SystemInfo.supportsGyroscope == false)
        {
            gyroButton.interactable = false;
        }
    }

    void Update()
    {
        if (Input.gyro.enabled)
        {
            gyroButton.Select();
        }
        else
        {
            touchButton.Select();
        }
    }

    public void ClickTouch()
    {
        Input.gyro.enabled = false;
    }

    public void ClickGyro()
    {
        Input.gyro.enabled = true;
    }
}
