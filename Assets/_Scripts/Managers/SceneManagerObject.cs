using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerObject : MonoBehaviour
{
    public static SceneManagerObject Instance;
    [SerializeField] private float timeBtwnSceneTransitions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(ChangeSceneCoroutine(sceneIndex));
    }

    public void LoadNextScene()
    {
        int actualSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = actualSceneIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        StartCoroutine(ChangeSceneCoroutine(nextSceneIndex));
    }

    public void ReloadScene()
    {
        StartCoroutine(ChangeSceneCoroutine(SceneManager.GetActiveScene().buildIndex));
    }

    public void ReloadScene(float waitTime)
    {
        StartCoroutine(ReloadSceneWaitTime(waitTime));
    }

    public IEnumerator ChangeSceneCoroutine(int sceneIndex)
    {
        float transitionTime = SceneTransitionUI.Instance.ExitSceneCross();

        yield return new WaitForSeconds(timeBtwnSceneTransitions + transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator ReloadSceneWaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        ReloadScene();
    }
}
