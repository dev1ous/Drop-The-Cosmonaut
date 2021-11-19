using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public enum selectedOption
    {
        Login,
        Register
    }

    public selectedOption selected = selectedOption.Login;

    string loginUsername = "";
    string loginPassword = "";
    string registerUsername = "";
    string registerPassword = "";
    string confirmPassword = "";
    string errorMessage = "";

    public string Username = "";

    public string url = "";

    public bool isLogged = false;
    public bool isRegistered = false;
    public bool isLoading = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (!isLogged)
        {
            if(selected == selectedOption.Login)
            {
                GUI.Window(0, new Rect(Screen.width / 2 -75, 
                    Screen.height / 2 - 105, 150, 230), LoginFunc, "Login");
            }
            else if(selected == selectedOption.Register)
            {
                GUI.Window(0, new Rect(Screen.width / 2 -75, 
                    Screen.height / 2 - 105, 50, 330), RegisterFunc, "Register");
            }
        }

        GUI.Label(new Rect(22, 5, 500, 25), "Status: " + (isLogged ? Username.ToString() + "logged ..." : "Logged out"));

        if (isLogged)
        {
            if (GUI.Button(new Rect(5, 30, 100, 25), "Log out"))
            {
                isLogged = false;
                Username = "";
                selected = selectedOption.Login;
            }
        }
    }

    void LoginFunc(int idx)
    {
        if (isLoading)
        {
            GUI.enabled = false;
        }

        if(errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }
        if (isRegistered)
        {
            GUI.color = Color.green;
            GUILayout.Label("You have been successfully registered");
        }

        GUI.color = Color.white;
        GUILayout.Label("Username: ");
        loginUsername = GUILayout.TextField(loginUsername);
        GUILayout.Label("Password: ");
        loginPassword = GUILayout.PasswordField(loginPassword, '*');

        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(85)))
        {
            StartCoroutine(LoginNetwork());
        }

        GUILayout.FlexibleSpace();
        GUILayout.Label("Do you have an account?");

        if (GUILayout.Button("Register", GUILayout.Width(125)))
        {
            ResetInfosData();
            selected = selectedOption.Register;
        }
    }

    void RegisterFunc(int idx)
    {
        if (isLoading)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }

        GUI.color = Color.white;
        GUILayout.Label("Username: ");
        registerUsername = GUILayout.TextField(registerUsername, 254);
        GUILayout.Label("Password: ");
        registerPassword = GUILayout.PasswordField(registerPassword, '*', 19);
        GUILayout.Label("Confirm Password: ");
        confirmPassword = GUILayout.PasswordField(confirmPassword, '*', 19);

        GUILayout.Space(5);

        if(GUILayout.Button("Submit", GUILayout.Width(85)))
        {
            StartCoroutine(RegisterNetwork());
        }

        GUILayout.FlexibleSpace();
        GUILayout.Label("Have you already an account ?");
        if(GUILayout.Button("Login", GUILayout.Width(125)))
        {
            ResetInfosData();
            selected = selectedOption.Login;
        }
    }

    IEnumerator RegisterNetwork()
    {
        isLoading = true;
        isRegistered = false;
        errorMessage = "";

        WWWForm wWWForm = new();
        wWWForm.AddField("username", registerUsername);
        wWWForm.AddField("password", registerPassword);
        wWWForm.AddField("confirmation", confirmPassword);

        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(url + "register.php", wWWForm))
        {
            yield return unityWebRequest.SendWebRequest();


            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                errorMessage = unityWebRequest.error;
            }
            else
            {
                string responseText = unityWebRequest.downloadHandler.text;
                if (responseText.StartsWith("Success"))
                {
                    ResetInfosData();
                    isRegistered = true;
                    selected = selectedOption.Login;
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }
        isLoading = false;
    }

    IEnumerator LoginNetwork()
    {
        isLoading = true;
        isRegistered = false;
        errorMessage = "";

        WWWForm wWWForm = new();
        wWWForm.AddField("username", loginUsername);
        wWWForm.AddField("password", loginPassword);

        using(UnityWebRequest unityWeb = UnityWebRequest.Post(url + "login.php", wWWForm))
        {
            yield return unityWeb.SendWebRequest();
            if(unityWeb.result != UnityWebRequest.Result.Success)
            {
                errorMessage = unityWeb.error;
            }
            else
            {
                string responseText = unityWeb.downloadHandler.text;
                if(responseText.StartsWith("Success"))
                {
                    isLogged = true;
                    ResetInfosData();
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }
        isLoading = false;
    }

    private void ResetInfosData()
    {
        loginUsername = "";
        loginPassword = "";
        registerUsername = "";
        registerPassword = "";
        confirmPassword = "";
        errorMessage = "";
    }
}
