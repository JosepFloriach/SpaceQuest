using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textContainer;

    [SerializeField] private float changeColorThreshold;

    [ColorUsageAttribute(true, true)]
    [SerializeField] private Color normalColor;
    [ColorUsageAttribute(true, true)]
    [SerializeField] private Color lastSecondsColor;
    
    private LevelTimer levelTimer;

    // Start is called before the first frame update
    void Start()
    {
        levelTimer = FindObjectOfType<LevelTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelTimer.InfiniteTime)
        {
            textContainer.text = "";
        }
        else
        {
            if (levelTimer.CurrentTime < changeColorThreshold)
            {
                textContainer.fontMaterial.SetColor("_FaceColor", lastSecondsColor);
            }
            else
            {
                textContainer.fontMaterial.SetColor("_FaceColor", normalColor);
            }
            textContainer.text = levelTimer.CurrentTime.ToString("0");
        }
    }
}
