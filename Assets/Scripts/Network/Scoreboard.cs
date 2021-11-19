using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] Login login;
    Vector2 scroll = Vector2.zero;
    bool showScoreboard = false;
    int currentScore = 0;
    int previousScore = 0;
    float delayScoreSubmit;
    bool submitScore = false;
    int highestScore = 0;
    int playerRank = -1;
    [System.Serializable]
    public class ScoreboardUser
    {
        public string username;
        public int score;
    }
    ScoreboardUser[] scoreboardUsers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (login.isLogged)
        {
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
    }

    IEnumerator SubmitScore(int score)
    {
        submitScore = true;


        WWWForm wWWForm = new();
        wWWForm.AddField("username", login.Username);
        wWWForm.AddField("score", score);

        using (UnityWebRequest unityWeb = UnityWebRequest.Post(login.url + "score.php", wWWForm))
        {
            yield return unityWeb.SendWebRequest();

            if (unityWeb.result == UnityWebRequest.Result.ConnectionError)
            {
                print(unityWeb.error);
            }
            else
            {
                string responseText = unityWeb.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    print("New Score submitted");
                }
                else
                {
                    print(responseText);
                }
            }
        }
        submitScore = false;
    }

    IEnumerator GetScoreBoard()
    {
        login.isLoading = true;

        WWWForm wWWForm = new();
        wWWForm.AddField("username", login.Username);

        using(UnityWebRequest unityWeb = UnityWebRequest.Post(login.url + "scoreboard.php", wWWForm))
        {
            yield return unityWeb.SendWebRequest();
            if (unityWeb.result == UnityWebRequest.Result.ConnectionError)
            {
                print(unityWeb.error);
            }
            else
            {
                string responseText = unityWeb.downloadHandler.text;

                if (responseText.StartsWith("User"))
                {
                    string[] dataChunks = responseText.Split("|");

                    if (dataChunks[0].Contains(","))
                    {
                        string[] tmp = dataChunks[0].Split(',');
                        highestScore = int.Parse(tmp[1]);
                        playerRank = int.Parse(tmp[2]);
                    }
                    else
                    {
                        highestScore = 0;
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
                }
                else
                {
                    print(responseText);
                }
            }
        }
        login.isLoading = false;
    }

    private void OnGUI()
    {
        if (showScoreboard)
        {
            GUI.Window(1, new Rect(Screen.width / 2 - 300, Screen.height / 2 - 225, 600, 460), ScoreBoardRendering, "ScoreBoard");
        }
        if (!login.isLogged)
        {
            showScoreboard = false;
            currentScore = 0;
        }
        else
        {
            GUI.Box(new Rect(Screen.width / 2 - 65, 5, 120, 25), currentScore.ToString());
            if(GUI.Button(new Rect(5, 60, 100, 25), "ScoreBoard"))
            {
                showScoreboard = !showScoreboard;
                if (!login.isLoading)
                {
                    StartCoroutine(GetScoreBoard());
                }
            }
        }
    }

    void ScoreBoardRendering(int idx)
    {
        if (login.isLoading)
        {
            GUILayout.Label("Loading...");
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUI.color = Color.green;
            GUILayout.Label("Your Rank is " + (playerRank > 0 ? playerRank.ToString() : "not ranked"));
            GUILayout.Label("Highest Score: " + highestScore.ToString());
            GUI.color = Color.white;
            GUILayout.EndHorizontal();

            scroll = GUILayout.BeginScrollView(scroll, false, true);

            for(var i = 0; i < scoreboardUsers.Length; i++)
            {
                GUILayout.BeginHorizontal("box");

                if (scoreboardUsers[i].username == login.Username)
                {
                    GUI.color = Color.green;
                }
                GUILayout.Label((i + 1).ToString(), GUILayout.Width(30));
                GUILayout.Label(scoreboardUsers[i].username, GUILayout.Width(230));

                GUILayout.Label(scoreboardUsers[i].score.ToString());
                GUI.color = Color.white;
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
        }
    }
}
