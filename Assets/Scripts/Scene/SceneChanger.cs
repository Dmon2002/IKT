using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : Manager<SceneChanger> {

    private Animator _animator;
    private AsyncOperation _loadingSceneOperation;

    private void Start()
    {
        _animator= GetComponent<Animator>();
    }


    public static void ChangeScene(string sceneName) 
    { 
        Instance._animator.SetTrigger("SceneClosing");
        Instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        Instance._loadingSceneOperation.allowSceneActivation = false;
    }


    public static void ChangeScene(int sceneNum)
    {

        Instance._animator.SetTrigger("SceneClosing");
        Instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneNum);
        Instance._loadingSceneOperation.allowSceneActivation = false;
    }

    public void OnClosingAnimationOver()
    {
        Instance._loadingSceneOperation.allowSceneActivation = true;
        Instance._animator.SetTrigger("SceneOpening");
    }


    public void Exit() 
    {
        Application.Quit(); 
    } 
}
