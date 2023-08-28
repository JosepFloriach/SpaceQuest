using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class SoundsPool : MonoBehaviour
{
    [SerializeField] List<SoundSetup> sounds;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        ReferenceValidator.NotNull(soundManager, sounds);
    }

    private void Start()
    {
        foreach (SoundSetup sound in sounds)
        {
            soundManager.RegisterSound(sound);
        }
    }
}
