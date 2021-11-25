using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text textScore;
    public int score;
    public int distTravelled;
    public int multiplier = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        score = distTravelled * multiplier;
        textScore.text = score.ToString();
    }
}
