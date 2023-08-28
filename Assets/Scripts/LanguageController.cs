using jovetools.gameserialization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageController : SingletonMonoBehaviour<LanguageController>, ISerializable
{
    public int CurrentLanguage
    {
        get;
        private set;
    }

    public event Action changedLanguage;

    public void OnDestroy()
    {
        PersistanceManager<GameData>.Instance.DeregisterSerializableObject(this);
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(ChangeLanguage());
    }

    public void ChangeLanguage(int languageIdx)
    {
        CurrentLanguage = languageIdx;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIdx];
        PersistanceManager<GameData>.Instance.SaveGame();
        changedLanguage?.Invoke();
    }

    private IEnumerator ChangeLanguage()
    {
        while(!LocalizationSettings.InitializationOperation.IsDone)
        {
            yield return null;
        }
        ChangeLanguage(CurrentLanguage);
    }

    public void ClearData(IGameData data)
    {
        // Language is unaffected by reset progress.
    }

    public void CreateData(ref IGameData data)
    {
        GameData gameData = (GameData)data;        
        gameData.GameSetup.Language = 0;
    }

    public void LoadData(IGameData data)
    {
        GameData gameData = (GameData)data;
        CurrentLanguage = gameData.GameSetup.Language;
    }

    public void SaveData(ref IGameData data)
    {
        GameData gameData = (GameData)data;
        gameData.GameSetup.Language = CurrentLanguage;
    }
}
