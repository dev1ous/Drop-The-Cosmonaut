using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
public class ForceAcceptAll : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}
public class Login : Network
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
    //GUIStyle style = new GUIStyle();

    //[SerializeField] private float scale = 1f;

    [SerializeField] private GameObject loginPanel = null;
    [SerializeField] private GameObject registerPanel = null;

    [SerializeField] private InputField loginUsernameField = null;
    [SerializeField] private InputField loginPasswordField = null;

    [SerializeField] private InputField registerUsernameField = null;
    [SerializeField] private InputField registerPasswordField = null;
    [SerializeField] private InputField registerPasswordConfirmField = null;

    [SerializeField] private Text statutText = null;
    [SerializeField] private Text errorText = null;

    private float registerTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ConnectToDb());
    }

    // Update is called once per frame
    void Update()
    {
        statutText.text = "Status: " + (isLogged ? Username.ToString() + "logged ..." : "Logged out");
        errorText.text = errorMessage;

        if (isLogged)
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (isRegistered)
        {
            registerTimer += Time.deltaTime;

            if (registerTimer >= 1f)
            {
                loginPanel.SetActive(true);
                registerPanel.SetActive(false);
                errorMessage = "";
                errorText.color = Color.red;
            }
        }
    }

    IEnumerator ConnectToDb()
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(url + "database.php", ""))
        {
            ForceAcceptAll cert = new ForceAcceptAll();
            unityWebRequest.certificateHandler = cert;
            Debug.Log(url + "database.php");
            yield return unityWebRequest.SendWebRequest();
            cert?.Dispose();

            if (unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("CONNECTED");
            }
            else
            {
                Debug.LogError(unityWebRequest.error);
            }

        }
    }

    private void OnGUI()
    {
        //style.fontSize = 10 * (int)scale;

        //if (!isLogged)
        //{
        //    if (selected == selectedOption.Login)
        //    {
        //        GUI.Window(0, new Rect(Screen.width / 2 - (150 * scale) / 2f,
        //            Screen.height / 2 - (230 * scale) / 2f, 150 * scale, 230 * scale), LoginFunc, "Login");
        //    }
        //    else if (selected == selectedOption.Register)
        //    {
        //        GUI.Window(0, new Rect(Screen.width / 2 - (150 * scale) / 2f,
        //            Screen.height / 2 - (260 * scale) / 2f, 150 * scale, 260 * scale), RegisterFunc, "Register");
        //    }
        //    GUI.skin.window.fontSize = 10 * (int)scale;
        //    GUI.skin.window.border.top = 10;
        //}

        //GUI.Label(new Rect(22, 5, 500 * scale, 25 * scale), "Status: " + (isLogged ? Username.ToString() + "logged ..." : "Logged out"), style);

        //if (isLogged)
        //{
        //    if (GUI.Button(new Rect(5, 30, 100 * scale, 25 * scale), "Log out"))
        //    {
        //        isLogged = false;
        //        Username = "";
        //        selected = selectedOption.Login;
        //    }
        //}
    }

    //void LoginFunc(int idx)
    //{
    //    if (isLoading)
    //    {
    //        GUI.enabled = false;
    //    }

    //    if(errorMessage != "")
    //    {
    //        GUI.color = Color.red;
    //        GUILayout.Label(errorMessage);
    //    }
    //    if (isRegistered)
    //    {
    //        GUI.color = Color.green;
    //        GUILayout.Label("You have been successfully registered");
    //    }

    //    GUI.color = Color.white;
    //    GUILayout.Label("Username: ");
    //    loginUsername = GUILayout.TextField(loginUsername);
    //    GUILayout.Label("Password: ");
    //    loginPassword = GUILayout.PasswordField(loginPassword, '*');

    //    GUILayout.Space(5);

    //    if (GUILayout.Button("Submit", GUILayout.Width(85 * scale)))
    //    {
    //        StartCoroutine(LoginNetwork());
    //    }

    //    GUILayout.FlexibleSpace();
    //    GUILayout.Label("Didn't have an account yet?");

    //    if (GUILayout.Button("Register", GUILayout.Width(125 * scale)))
    //    {
    //        ResetInfosData();
    //        selected = selectedOption.Register;
    //    }
    //}

    //void RegisterFunc(int idx)
    //{
    //    if (isLoading)
    //        GUI.enabled = false;

    //    if (errorMessage != "")
    //    {
    //        GUI.color = Color.red;
    //        GUILayout.Label(errorMessage);
    //    }

    //    GUI.color = Color.white;
    //    GUILayout.Label("Username: ");
    //    registerUsername = GUILayout.TextField(registerUsername, 254);
    //    GUILayout.Label("Password: ");
    //    registerPassword = GUILayout.PasswordField(registerPassword, '*', 19);
    //    GUILayout.Label("Confirm Password: ");
    //    confirmPassword = GUILayout.PasswordField(confirmPassword, '*', 19);

    //    GUILayout.Space(5);

    //    if(GUILayout.Button("Submit", GUILayout.Width(85 * scale)))
    //    {
    //        StartCoroutine(RegisterNetwork());
    //    }

    //    if(GUILayout.Button("Return to login", GUILayout.Width(130 * scale), GUILayout.Height(50 * scale)))
    //    {
    //        ResetInfosData();
    //        selected = selectedOption.Login;
    //    }
    //}

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
            ForceAcceptAll cert = new ForceAcceptAll();
            unityWebRequest.certificateHandler = cert;
            yield return unityWebRequest.SendWebRequest();
            cert?.Dispose();

            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                errorMessage = unityWebRequest.error;
            }
            else
            {
                string responseText = unityWebRequest.downloadHandler.text;
                if (responseText.StartsWith("Registration completed"))
                {
                    ResetInfosData();
                    registerTimer = 0f;
                    isRegistered = true;
                    errorMessage = responseText;
                    errorText.color = Color.green;
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
        wWWForm.AddField("highscores", highestScore);

        using(UnityWebRequest unityWeb = UnityWebRequest.Post(url + "login.php", wWWForm))
        {
            ForceAcceptAll cert = new();
            unityWeb.certificateHandler = cert;
            yield return unityWeb.SendWebRequest();

            cert?.Dispose();
            if(unityWeb.result != UnityWebRequest.Result.Success)
            {
                errorMessage = unityWeb.error;
            }
            else
            {
                string responseText = unityWeb.downloadHandler.text;

                if(responseText.StartsWith("Success"))
                {
                    string[] arr = responseText.Split('|');
                    highestScore = int.Parse(arr[1]);
                    isLogged = true;
                    Username = loginUsername;
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
        isRegistered = false;
    }


    public void ClickSubmit()
    {
        StartCoroutine(LoginNetwork());
    }

    public void ClickRegister()
    {
        ResetInfosData();
        selected = selectedOption.Register;
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void ChangeUsername()
    {
        loginUsername = loginUsernameField.text;
    }

    public void ChangePassword()
    {
        loginPassword = loginPasswordField.text;
    }



    public void ChangeRegisterUsername()
    {
        registerUsername = registerUsernameField.text;
    }

    public void ChangeRegisterPassword()
    {
        registerPassword = registerPasswordField.text;
    }

    public void ChangeRegisterPasswordConfirm()
    {
        confirmPassword = registerPasswordConfirmField.text;
    }

    public void ClickRegisterSubmit()
    {
        StartCoroutine(RegisterNetwork());
    }

    public void ClickRegisterLogin()
    {
        ResetInfosData();
        selected = selectedOption.Login;
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }
}
