using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource buttonClick;
    private Button[] buttons;

    void Start()
    {
        buttons = FindObjectsOfType<MonoBehaviour>().OfType<Button>().ToArray();
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => { 
                buttonClick.Play();
            });
        }
    }
}
