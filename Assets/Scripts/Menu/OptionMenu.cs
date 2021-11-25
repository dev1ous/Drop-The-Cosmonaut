using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public class Settings
    {
        public float volume = 0f;
        public bool vibrationEnabled = false;
        public bool gyroEnabled = false;
    }

    [SerializeField] private Button touchButton = null;
    [SerializeField] private Button gyroButton = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Toggle vibrationToggle = null;

    public static Settings settings = new Settings();

    void Start()
    {
        settings.volume = PlayerPrefs.GetFloat("Volume");
        settings.gyroEnabled = System.Convert.ToBoolean(PlayerPrefs.GetInt("GyroEnabled"));
        settings.vibrationEnabled = System.Convert.ToBoolean(PlayerPrefs.GetInt("VibrationEnabled"));

        if (SystemInfo.supportsGyroscope == false)
        {
            gyroButton.interactable = false;
            settings.gyroEnabled = false;
        }
        else
        {
            Input.gyro.enabled = settings.gyroEnabled;
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

        vibrationToggle.isOn = settings.vibrationEnabled;
        volumeSlider.value = settings.volume;

        settings.gyroEnabled = Input.gyro.enabled;
    }

    public void ClickTouch()
    {
        Input.gyro.enabled = false;
    }

    public void ClickGyro()
    {
        Input.gyro.enabled = true;
    }
    public void ChangeVolume()
    {
        settings.volume = volumeSlider.value;
    }

    public void ClickVibration()
    {
        settings.vibrationEnabled = vibrationToggle.isOn;
    }

    public static void Save()
    {
        PlayerPrefs.SetFloat("Volume", settings.volume);
        PlayerPrefs.SetInt("GyroEnabled", System.Convert.ToInt32(settings.gyroEnabled));
        PlayerPrefs.SetInt("VibrationEnabled", System.Convert.ToInt32(settings.vibrationEnabled));
    }
}
