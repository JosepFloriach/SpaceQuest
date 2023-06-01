using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuit : MonoBehaviour
{
    public void QuitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
