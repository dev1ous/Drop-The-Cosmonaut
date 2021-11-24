using UnityEngine;

public class State : MonoBehaviour
{
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

        //float progressValue = Mathf.Clamp01(stateMachine.asyncLoad.progress / 0.9f);
        //percentLoaded.text = Mathf.Round(progressValue * 100) + "%";

            stateMachine.triggerEnter.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stateMachine.asyncLoad == null)
            return;
            if (stateMachine.asyncLoad.progress >= 0.9f)
                stateMachine.triggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
            stateMachine.triggerExit.Invoke();
    }
}
