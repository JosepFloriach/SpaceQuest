using jovetools.gameserialization;
using TMPro;
using UnityEngine;

public class SetupPanelUI : MonoBehaviour
{
    [SerializeField] TMP_Dropdown languageSelector;
    [SerializeField] TMP_Dropdown qualitySelector;

    private LanguageController languageController;
    private QualitySettingsController qualitySettingsController;

    private void Awake()
    {
        languageController = FindObjectOfType<LanguageController>();
        qualitySettingsController = FindObjectOfType<QualitySettingsController>();
        ReferenceValidator.NotNull(
            languageController, 
            qualitySettingsController, 
            languageSelector, 
            qualitySelector);

        languageSelector.onValueChanged.AddListener(delegate
        {
            OnLanguageChanged(languageSelector);
        });
    }

    public void Start()
    {
        languageSelector.value = languageController.CurrentLanguage;
        qualitySelector.value = qualitySettingsController.CurrentSettings;
    }

    public void OnLanguageChanged(TMP_Dropdown dropDown)
    {
        languageController.ChangeLanguage(dropDown.value);
    }

    public void OnQualitySettingsChanged(TMP_Dropdown dropDown)
    {
        qualitySettingsController.ChangeSettings(dropDown.value);
    }

    public void OnResetData()
    {
        PersistanceManager<GameData>.Instance.DeleteGame();
    }
}
