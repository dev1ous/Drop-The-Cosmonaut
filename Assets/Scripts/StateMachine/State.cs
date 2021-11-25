using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField]
    StateMachine stateMachine;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other)
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
