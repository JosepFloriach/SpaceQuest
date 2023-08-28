using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpEngine : MonoBehaviour
{
    [SerializeField] private float slowDownFactor = 0.05f;

    public bool IsEnabled { get; private set; }

    public void Enable()
    {
        if (IsEnabled)
            return;

        Time.timeScale = slowDownFactor;
        //Time.fixedDeltaTime = Time.timeScale * 0.2f;
        IsEnabled = true;
    }

    public void Disable()
    {
        if (!IsEnabled)
            return;

        Time.timeScale = 1.0f;
        //Time.fixedDeltaTime = Time.timeScale * 0.2f;
        IsEnabled = false;
    }
}
