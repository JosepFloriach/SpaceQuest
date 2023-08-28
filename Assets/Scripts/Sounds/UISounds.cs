using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        ReferenceValidator.NotNull(soundManager);
    }

    public void PlayChangeMessageSound()
    {
        soundManager.PlaySound("Click");
    }

    public void PlayDialogOpenedSound()
    {
        soundManager.PlaySound("DialogOpen");
    }

    public void PlayDialogClosedSound()
    {
        soundManager.PlaySound("DialogOpen");
    }

    public void PlayGameOverSound()
    {
        soundManager.PlaySound("GameOver");
    }

    public void PlayLevelCompleteSound()
    {
        soundManager.PlaySound("LevelComplete");
    }

    public void StopBackground()
    {
        soundManager.StopSound("Background");
    }

    public void PlayBackground()
    {
        soundManager.PlaySound("Background");
    }

}
