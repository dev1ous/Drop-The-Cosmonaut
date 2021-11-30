using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text usernameText = null;
    [SerializeField] private Text scoreText = null;

    public void ClickFall()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickLogOff()
    {
        Network.isLoginOut = true;
        Network.Username = "";
        Network.isLogged = false;
        Network.isLoading = false;
        Network.isRegistered = false;
        SceneManager.LoadScene("Login");
    }

    private void Update()
    {
        usernameText.text = Network.Username;
        scoreText.text = Network.highestScore.ToString();
    }
}
