using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Button touchButton = null;
    [SerializeField] private Button gyroButton = null;

    private Image touchImage = null;
    private Image gyroImage = null;

    void Start()
    {
        if (SystemInfo.supportsGyroscope == false)
        {
            gyroButton.interactable = false;
        }

        touchImage = touchButton.GetComponent<Image>();
        gyroImage = gyroButton.GetComponent<Image>();
    }

    void Update()
    {
        if (Input.gyro.enabled)
        {
            touchImage.color = Color.white;
            gyroImage.color = Color.cyan;
        }
        else
        {
            touchImage.color = Color.cyan;
            gyroImage.color = Color.white;
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
