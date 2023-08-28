using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickLevel : MonoBehaviour
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
}
