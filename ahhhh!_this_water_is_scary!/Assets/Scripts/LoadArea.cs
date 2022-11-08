using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadArea : MonoBehaviour
{
    //[SerializeField] private string sceneName;

    public void LoadSection(string sceneName)
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadSection(string sceneName)
    {
        AsyncOperation unloadAsync = SceneManager.UnloadSceneAsync(sceneName);
    }
}