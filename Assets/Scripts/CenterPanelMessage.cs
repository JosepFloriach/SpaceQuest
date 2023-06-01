using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CenterPanelMessage: MonoBehaviour
{
    [ColorUsageAttribute(true, true)]
    [SerializeField] private Color color;
    [SerializeField] private float glowPower;
    
    private Material instanceMaterial;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        instanceMaterial = text.fontMaterial;
    }

    void Start()
    {
        instanceMaterial.SetFloat("_GlowPower", glowPower);
        instanceMaterial.SetColor("_FaceColor", new Color(color.r, color.g, color.b));      
    }
}
