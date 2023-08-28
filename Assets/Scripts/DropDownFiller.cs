using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DropDownFiller : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown dropDown;
    [SerializeField] private List<LocalizedString> options = new();

    
    private void OnEnable()
    {
        FindObjectOfType<LanguageController>().changedLanguage += UpdateDropDownOptions;
    }

    private void OnDisable()
    {
        FindObjectOfType<LanguageController>().changedLanguage -= UpdateDropDownOptions;
    }

    private void UpdateDropDownOptions()
    {
        int previousSelected = FindObjectOfType<LanguageController>().CurrentLanguage;
        List<string> stringOptions = new();
        foreach (var option in options)
        {
            stringOptions.Add(option.GetLocalizedString());
        }
        dropDown.ClearOptions();
        dropDown.AddOptions(stringOptions);
        dropDown.value = previousSelected;
    }
}
