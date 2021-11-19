using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State : MonoBehaviour
{
    public UnityEvent TriggerEnter = new UnityEvent();
    public UnityEvent TriggerExit = new UnityEvent();
    StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (stateMachine.asyncLoad == null)
            return;

        float progressValue = Mathf.Clamp01(stateMachine.asyncLoad.progress / 0.9f);
        //percentLoaded.text = Mathf.Round(progressValue * 100) + "%";

            TriggerEnter.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stateMachine.asyncLoad == null)
            return;
            if (stateMachine.asyncLoad.progress >= 0.9f)
                TriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
            TriggerExit.Invoke();
    }
}
