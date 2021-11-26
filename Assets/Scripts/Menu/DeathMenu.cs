using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private AudioSource deathSound;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        deathSound.Play();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void ClickRetry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
