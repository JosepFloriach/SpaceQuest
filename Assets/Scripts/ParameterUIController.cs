using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterUIController : MonoBehaviour
{
    [SerializeField] List<Image> ticks;
    private int currentValue;

    public void SetValue(int value)
    {
        currentValue = value;
    }

    private void Update()
    {
        for(int idx = 0; idx < ticks.Count; ++idx)
        {
            if (currentValue > idx)
            {
                ticks[idx].enabled = true;
            }
            else
            {
                ticks[idx].enabled = false;
            }
        }
    }
}
