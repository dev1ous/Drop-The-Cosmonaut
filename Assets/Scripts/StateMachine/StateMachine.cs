using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachine : MonoBehaviour
{
    public AsyncOperation asyncLoad;
    public UnityEvent triggerEvent;
    public UnityEvent triggerEventForExit;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartScene(string sceneName, string spawn)
    {
        StartCoroutine(LoadNextScene(sceneName, spawn));
    }

    public void UnloadPreviousScene()
    {
        if (asyncLoad.allowSceneActivation)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene(), UnloadSceneOptions.None);
            if (triggerEvent != null)
            {
                triggerEvent.RemoveAllListeners();
                triggerEventForExit.RemoveAllListeners();
                triggerEvent = null;
                triggerEventForExit = null;
            }
        }


    }

    private void EnableScene()
    {
        asyncLoad.allowSceneActivation = true;
    }

    IEnumerator LoadNextScene(string sceneName, string spawn)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            //if (asyncLoad.progress >= 0.9f)
                
            if (triggerEvent != null)
                triggerEvent.AddListener(EnableScene);
            if (triggerEventForExit != null)
                triggerEventForExit.AddListener(UnloadPreviousScene);

            yield return null;
        }
    }
}
