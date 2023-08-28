using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitSounds : MonoBehaviour
{
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        ReferenceValidator.NotNull(soundManager);
    }

    public void PlaySpawnSound()
    {
        soundManager.PlaySound("Spawn");
    }

    public void PlayVerticalThrusterSound()
    {
        soundManager.PlaySound("VerticalThruster");
    }

    public void StopVerticalThrusterSound()
    {
        soundManager.StopSound("VerticalThruster");
    }

    public void PlayBackground()
    {
        soundManager.PlaySound("Background");
    }

    public void StopBackground()
    {
        soundManager.StopSound("Background");
    }
}
