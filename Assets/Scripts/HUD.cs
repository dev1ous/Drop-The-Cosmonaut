using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Character player = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private ParticleSystem scoreParticules = null;

    // Update is called once per frame
    void Update()
    {
        if (player.score > 1000f) // player.score >= highScore
        {
            scoreText.color = new Color(0.933f, 0.882f, 0.305f);
            scoreParticules.gameObject.SetActive(true);
        }
        else
        {
            scoreParticules.gameObject.SetActive(false);
        }
    }
}
