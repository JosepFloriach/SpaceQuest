using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string sceneName;
    public void SetScenePath(string sceneName)
    {
        this.sceneName = sceneName;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
