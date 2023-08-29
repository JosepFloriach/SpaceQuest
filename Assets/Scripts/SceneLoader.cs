using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string sceneName;

    private void Start()
    {
    }

    public void SetScenePath(string sceneName)
    {
        this.sceneName = sceneName;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenu()
    {
        var nameWithoutExtension = Path.GetFileNameWithoutExtension("Scenes/MainMenu_v2");
        SetScenePath(nameWithoutExtension);
        LoadScene();
    }
}
