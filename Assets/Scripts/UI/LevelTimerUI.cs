using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimerUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private LevelTimer levelTimer;

    private void Awake()
    {
        levelTimer = FindObjectOfType<LevelTimer>();
        ReferenceValidator.NotNull(slider, levelTimer);
    }

    private void Update()
    {
        if (levelTimer.InfiniteTime)
        {
            slider.value = 1.0f;

        }
        else
        {
            slider.value = levelTimer.CurrentTime / levelTimer.MaxTime;
        }
    }
}
