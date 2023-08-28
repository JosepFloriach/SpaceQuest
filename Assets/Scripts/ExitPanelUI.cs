using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject exitPanel;

    private KeyboardControls keyboardControls;
    private SceneLoader sceneLoader;
    private LevelFreezer levelFreezer;

    private bool trigger = false;

    public void OnExitClicked()
    {
        sceneLoader.LoadMainMenu();
    }

    private void Awake()
    {
        keyboardControls = FindObjectOfType<KeyboardControls>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        levelFreezer = FindObjectOfType<LevelFreezer>();
        ReferenceValidator.NotNull(keyboardControls, sceneLoader, levelFreezer, exitPanel);
    }

    private void Start()
    {
        trigger = false;
        exitPanel.SetActive(false);
    }

    private void OnEnable()
    {
        keyboardControls.PressedScape += OnScapePressed;
    }

    private void OnDisable()
    {
        keyboardControls.PressedScape -= OnScapePressed;
    }

    private void OnScapePressed()
    {
        trigger = !trigger;
        if (trigger)
        {
            levelFreezer.Freeze();
        }
        else
        {
            levelFreezer.Unfreeze();
        }        
        exitPanel.SetActive(trigger);
    }
}
