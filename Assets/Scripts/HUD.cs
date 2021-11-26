using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Character player = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text highscoreText = null;
    [SerializeField] private ParticleSystem scoreParticules = null;

    private bool highscoreEnable = false;
    private float highscoreTimer = 0f;
    private float highscoreDefaultY = 0f;

    private void Start()
    {
        highscoreDefaultY = highscoreText.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.score > Network.highestScore)
        {
            scoreText.color = new Color(0.933f, 0.882f, 0.305f);
            scoreParticules.gameObject.SetActive(true);
            highscoreText.gameObject.SetActive(true);
            highscoreEnable = true;
            highscoreText.transform.localPosition = new Vector3(highscoreText.transform.localPosition.x, highscoreDefaultY + (highscoreTimer * -100f), highscoreText.transform.localPosition.z);
        }
        else
        {
            scoreParticules.gameObject.SetActive(false);
            highscoreText.gameObject.SetActive(false);
            highscoreEnable = false;
            highscoreTimer = 0f;
            highscoreText.transform.localPosition = new Vector3(highscoreText.transform.localPosition.x, highscoreDefaultY, highscoreText.transform.localPosition.z);
        }


        if (highscoreEnable)
        {
            highscoreTimer += Time.deltaTime * 0.6f;
        }

        if (highscoreTimer < 2f)
        {
            highscoreText.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            highscoreTimer = Mathf.Clamp(highscoreTimer, 0f, 3f);
            highscoreText.color = new Color(1f, 1f, 1f, 1f - (highscoreTimer - 2f));
        }
    }
}
