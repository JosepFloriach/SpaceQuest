using jovetools.gameserialization;
using UnityEngine;

public class QualitySettingsController : SingletonMonoBehaviour<QualitySettingsController>, ISerializable
{
    public int CurrentSettings
    {
        get;
        private set;
    }

    public void OnDestroy()
    {
        PersistanceManager<GameData>.Instance.DeregisterSerializableObject(this);
    }

    public void ChangeSettings(int settingsIdx)
    {
        CurrentSettings = settingsIdx;
        QualitySettings.SetQualityLevel(settingsIdx, true);
        PersistanceManager<GameData>.Instance.SaveGame();
    }

    public void ClearData(IGameData data)
    {
        // Qualitty settings is unaffected by reset progress.
    }

    public void CreateData(ref IGameData data)
    {
        GameData gameData = (GameData)data;
        gameData.GameSetup.GraphicsQuality = 2;
    }

    public void LoadData(IGameData data)
    {
        GameData gameData = (GameData)data;
        CurrentSettings = gameData.GameSetup.GraphicsQuality;
        QualitySettings.SetQualityLevel(CurrentSettings, true);        
    }

    public void SaveData(ref IGameData data)
    {
        GameData gameData = (GameData)data;
        gameData.GameSetup.GraphicsQuality= CurrentSettings;
    }
}
