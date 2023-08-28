using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundHelpers : MonoBehaviour
{
    private SoundManager soundManager;
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        ReferenceValidator.NotNull(soundManager);
    }

    public void PlayClickSound()
    {
        soundManager.PlaySound("Click");
    }

    public void PlayBuySound()
    {
        soundManager.PlaySound("BuyClick");
    }

    public void PlaySelectSound()
    {
        soundManager.PlaySound("SelectClick");
    }

    public void PlayStartPlaySound()
    {
        soundManager.PlaySound("StartPlay");
    }
}
