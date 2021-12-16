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

    private bool isRemember = false;

    [SerializeField] private GameObject loginPanel = null;
    [SerializeField] private GameObject registerPanel = null;

    [SerializeField] private InputField loginUsernameField = null;
    [SerializeField] private InputField loginPasswordField = null;

    [SerializeField] private InputField registerUsernameField = null;
    [SerializeField] private InputField registerPasswordField = null;
    [SerializeField] private InputField registerPasswordConfirmField = null;

    [SerializeField] private Toggle rememberMeToggle = null;

    [SerializeField] private Text statutText = null;
    [SerializeField] private Text errorText = null;
    [SerializeField] private Text LoginDBText = null;
    [SerializeField] private GameObject connectingPanel = null;

    private float registerTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (isLoginOut == false)
        {
            isLoginOut = true;

            isRemember = System.Convert.ToBoolean(PlayerPrefs.GetInt("isRemember"));
            rememberMeToggle.isOn = isRemember;

            if (isRemember)
            {
                loginUsername = PlayerPrefs.GetString("Username");
                loginPassword = PlayerPrefs.GetString("Password");

                loginUsernameField.text = loginUsername;
                loginPasswordField.text = loginPassword;
            }
        }

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

        if (isLoading)
        {
            loginUsernameField.interactable = false;
            loginPasswordField.interactable = false;
            registerUsernameField.interactable = false;
            registerPasswordField.interactable = false;
            registerPasswordConfirmField.interactable = false;
            rememberMeToggle.interactable = false;
        }
        else
        {
            loginUsernameField.interactable = true;
            loginPasswordField.interactable = true;
            registerUsernameField.interactable = true;
            registerPasswordField.interactable = true;
            registerPasswordConfirmField.interactable = true;
            rememberMeToggle.interactable = true;
        }
    }

    IEnumerator ConnectToDb()
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(url + "database.php", ""))
        {
            ForceAcceptAll cert = new ForceAcceptAll();
            unityWebRequest.certificateHandler = cert;
            //Debug.Log(url + "database.php");
            unityWebRequest.timeout = 5;
            yield return unityWebRequest.SendWebRequest();
            cert?.Dispose();

            if (unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                if (isRemember && loginUsername != "" && loginPassword != "")
                {
                    ClickSubmit();
                }

                connectingPanel.gameObject.SetActive(false);
                loginPanel.SetActive(true);
            }
            else
            {
                LoginDBText.text = unityWebRequest.error;
                LoginDBText.color = Color.red;
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

        using (UnityWebRequest unityWeb = UnityWebRequest.Post(url + "login.php", wWWForm))
        {
            ForceAcceptAll cert = new();
            unityWeb.certificateHandler = cert;
            yield return unityWeb.SendWebRequest();

            cert?.Dispose();
            if (unityWeb.result != UnityWebRequest.Result.Success)
            {
                errorMessage = unityWeb.error;
            }
            else
            {
                string responseText = unityWeb.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    string[] arr = responseText.Split('|');
                    highestScore = int.Parse(arr[1]);
                    isLogged = true;
                    Username = loginUsername;
                    SaveInfos();
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

    private void SaveInfos()
    {
        if (isRemember)
        {
            PlayerPrefs.SetInt("isRemember", 1);
            PlayerPrefs.SetString("Username", loginUsername);
            PlayerPrefs.SetString("Password", loginPassword);
        }
        else
        {
            PlayerPrefs.SetInt("isRemember", 0);
            PlayerPrefs.SetString("Username", "");
            PlayerPrefs.SetString("Password", "");
        }
    }

    public void ClickSubmit()
    {
        if (isLoading == false)
        {
            StartCoroutine(LoginNetwork());
        }
    }

    public void ClickRegister()
    {
        if (isLoading == false)
        {
            ResetInfosData();
            selected = selectedOption.Register;
            loginPanel.SetActive(false);
            registerPanel.SetActive(true);
        }
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
        if (isLoading == false)
        {
            StartCoroutine(RegisterNetwork());
        }
    }

    public void ClickRegisterLogin()
    {
        if (isLoading == false)
        {
            ResetInfosData();
            selected = selectedOption.Login;
            loginPanel.SetActive(true);
            registerPanel.SetActive(false);
        }
    }

    public void ClickRemember()
    {
        isRemember = rememberMeToggle.isOn;
    }

    public void ClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
