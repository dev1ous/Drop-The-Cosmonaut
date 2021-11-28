using UnityEngine.Networking;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : Network
{
    Vector2 scroll = Vector2.zero;
    [SerializeField] bool showScoreboard = false;
    int currentScore = 0;
    int previousScore = 0;
    float delayScoreSubmit;
    int bestScore = 0;
    bool submitScore = false;
    int playerRank = -1;
    bool scorebordResult = false;
    [SerializeField] DeathMenu deathMenu = null;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] Character player;
    [SerializeField] private GameObject panelScoreboard = null;
    [SerializeField] private Text Highscores = null;
    [SerializeField] private PanelScoreboard panelScoreboardPrefab = null;
    [SerializeField] private Transform basePosition = null;

    [System.Serializable]
    public class ScoreboardUser
    {
        public string username;
        public int score;
    }
    ScoreboardUser[] scoreboardUsers;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        deathMenu.gameObject.SetActive(true);
        if (isLogged)
        {
            currentScore = player.score;

            if(currentScore != previousScore && !submitScore)
            {
                if (delayScoreSubmit > 0)
                    delayScoreSubmit -= Time.deltaTime;
                else
                    previousScore = currentScore;
                StartCoroutine(SubmitScore(currentScore));
            }
        }
        else
        {
            delayScoreSubmit = 3;
        }
        //if (Input.GetKeyDown(KeyCode.T))
        //    currentScore += 5;

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(SubmitScore(currentScore));
        //}

        if (scorebordResult)
        {
            panelScoreboard.SetActive(true);
        }
        if (!isLogged)
            currentScore = 0;
        else
        {
            if (!scorebordResult)
                StartCoroutine(GetScoreBoard());
        }
    }

    //private void OnGUI()
    //{
    //    if (scorebordResult)
    //    {
    //        Rect rect = new(Screen.width / 2 - 450, Screen.height / 2 - 850, (450 * multiplier), (800 * multiplier));
    //        //windowTex.height = (int)rect.height;
    //        //windowTex.width = (int)rect.width;
    //        GUI.Window(1, rect, ScoreBoardRendering, new GUIContent("ScoreBoard"));
    //        GUI.skin.window.fontSize = 20 * (int)multiplier;
    //        GUI.skin.window.border.top = 20;
    //    }
    //    if (!isLogged)
    //    {
    //        //showScoreboard = false;
    //        currentScore = 0;
    //    }
    //    else
    //    {
    //        if (scorebordResult == false)
    //        {
    //            StartCoroutine(GetScoreBoard());
    //        }

    //        //GUI.Box(new Rect(Screen.width / 2 - 65, 5, 120, 25), currentScore.ToString());
    //        //if (GUI.Button(new Rect(5, 60, 100, 25), "ScoreBoard"))
    //        //{
    //        //    showScoreboard = !showScoreboard;
    //        //    if (!isLoading)
    //        //    {
    //        //    }
    //        //}
    //    }
    //}

    //private void ScoreBoardRendering(int idx)
    //{
    //    if (isLoading)
    //    {
    //        GUILayout.Label("Loading...");
    //    }
    //    else
    //    {
    //        GUILayout.Space(50f);
    //        GUILayout.BeginHorizontal();
    //        GUI.color = Color.green;
    //        GUILayout.Label("Your Rank is ");
    //        GUILayout.Space(100f);
    //        GUILayout.Label("Your highest Score: " + highestScore.ToString());
    //        GUI.skin.label.fontSize = 20 * (int)multiplier;
    //        GUI.color = Color.white;
    //        GUILayout.EndHorizontal();

    //        scroll = GUILayout.BeginScrollView(scroll, false, true);

    //        for(var i = 0; i < scoreboardUsers.Length; i++)
    //        {
    //            GUILayout.BeginHorizontal("box");

    //            if (scoreboardUsers[i].username == Username)
    //            {
    //                GUI.color = Color.green;
    //            }
    //            GUILayout.Label((i + 1).ToString(), GUILayout.Width(30));
    //            GUILayout.Label(scoreboardUsers[i].username, GUILayout.Width(230));
    //            GUILayout.Space(130f);
    //            GUILayout.Label(scoreboardUsers[i].score.ToString());
    //            GUI.color = Color.white;
    //            GUILayout.EndHorizontal();
    //        }
    //        GUILayout.EndScrollView();
    //    }
    //}

    private IEnumerator SubmitScore(int score)
    {
        submitScore = true;

        if (score > highestScore)
        {
            highestScore = score;
        }

        WWWForm wWWForm = new();
        wWWForm.AddField("username", Username);
        wWWForm.AddField("score", score);

        using (UnityWebRequest unityWeb = UnityWebRequest.Post(url + "submit_score.php", wWWForm))
        {
            ForceAcceptAll cert = new ForceAcceptAll();
            unityWeb.certificateHandler = cert;
            yield return unityWeb.SendWebRequest();
            cert?.Dispose();
            if (unityWeb.result == UnityWebRequest.Result.ConnectionError)
            {
                print(unityWeb.error);
            }
            else
            {
                string responseText = unityWeb.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    print( responseText + " : New Score submitted");
                }
                else
                {
                    print(responseText);
                }
            }
        }
        submitScore = false;
    }

    private IEnumerator GetScoreBoard()
    {
        scorebordResult = false;
        isLoading = true;

        WWWForm wWWForm = new();
        wWWForm.AddField("username", Username);
        //  wWWForm.AddField("username", "Martin");
        Debug.Log("user " + Username);

        using (UnityWebRequest unityWeb = UnityWebRequest.Post(url + "scoreboard.php", wWWForm))
        {
            ForceAcceptAll cert = new();
            unityWeb.certificateHandler = cert;
            yield return unityWeb.SendWebRequest();
            cert?.Dispose();
            if (unityWeb.result == UnityWebRequest.Result.ConnectionError)
            {
                print(unityWeb.error);
            }
            else
            {
                string responseText = unityWeb.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    string[] dataChunks = responseText.Split("|");
                    Debug.Log(responseText);

                    if (dataChunks[1].Contains(","))
                    {
                        string[] tmp = dataChunks[1].Split(',');
                        bestScore = int.Parse(tmp[1]);
                        //playerRank = int.Parse(tmp[2]);
                    }
                    else
                    {
                        bestScore = 0;
                        playerRank = -1;
                    }

                    scoreboardUsers = new ScoreboardUser[dataChunks.Length - 1];
                    for (var i = 1; i < dataChunks.Length; i++)
                    {
                        string[] tmp = dataChunks[i].Split(',');
                        ScoreboardUser user = new();
                        user.username = tmp[0];
                        user.score = int.Parse(tmp[1]);
                        scoreboardUsers[i - 1] = user;
                    }
                    scorebordResult = true;

                    for (var i = 0; i < scoreboardUsers.Length; i++)
                    {
                        PanelScoreboard tmp = Instantiate(panelScoreboardPrefab, panelScoreboard.transform);

                        if (scoreboardUsers[i].username == Username)
                        {
                            tmp.userRank.color = Color.green;
                            tmp.userName.color = Color.green;
                            tmp.userScore.color = Color.green;
                        }

                        tmp.userRank.text = (i + 1).ToString();
                        tmp.userName.text = scoreboardUsers[i].username;
                        tmp.userScore.text = scoreboardUsers[i].score.ToString();

                        tmp.transform.localPosition = new Vector3(0f, -130 * i + basePosition.localPosition.y, 0f);
                    }

                }
                else
                {
                    print(responseText);
                }
            }
        }
        isLoading = false;
    }
}
