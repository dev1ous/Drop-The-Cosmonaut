using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachine : MonoBehaviour
{
    public AsyncOperation asyncLoad;
    public UnityEvent triggerEnter;
    public UnityEvent triggerExit;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartScene(string sceneName)
    {
        StartCoroutine(LoadNextScene(sceneName));
    }

    public void UnloadPreviousScene()
    {
        if (asyncLoad.allowSceneActivation)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene(), UnloadSceneOptions.None);
            if (triggerEnter != null)
            {
                triggerEnter.RemoveAllListeners();
                triggerExit.RemoveAllListeners();
                triggerEnter = null;
                triggerExit = null;
            }
        }
    }

    private void EnableScene()
    {
        asyncLoad.allowSceneActivation = true;
    }

    IEnumerator LoadNextScene(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            //if (asyncLoad.progress >= 0.9f)
                
            if (triggerEnter != null)
                triggerEnter.AddListener(EnableScene);

            yield return null;
        }
    }
}
