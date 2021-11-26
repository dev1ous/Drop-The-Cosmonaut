using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ClickFall()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickLogOff()
    {
        Network.Username = "";
        Network.isLogged = false;
        Network.isLoading = false;
        Network.isRegistered = false;
        SceneManager.LoadScene("Login");
    }

}
