using jovetools.gameserialization;
using System.Collections.Generic;
using UnityEngine;
using static GameData.LevelProgressionData;

public class LevelProgressController : SingletonMonoBehaviour<LevelProgressController>, ISerializable
{
    [SerializeField] private List<string> levelNames;

    private List<LevelCompletion> levelsCompletion = new();

    public class LevelCompletion
    {
        public string LevelName;
        public int LevelIdx;
        public bool Completed;
        public List<bool> GemsCollected;
    }

    public int FurthestLevelCompleted
    {
        get;
        private set;
    }

    public int FurthestSystemCompleted
    {
        get;
        private set;
    }

    public void OnDestroy()
    {
        PersistanceManager<GameData>.Instance.DeregisterSerializableObject(this);
    }

    public void UpdateLevelProgress(LevelCompletion level)
    {
        levelsCompletion[level.LevelIdx].Completed = level.Completed;
        for (int idx = 0; idx < level.GemsCollected.Count; ++idx)
        {
            // If a gem was previously collected it does not matter if this time it has not been collected. It will keep collected.
            levelsCompletion[level.LevelIdx].GemsCollected[idx] |= level.GemsCollected[idx];
        }
        if (level.Completed)
        {
            FurthestLevelCompleted = Mathf.Max(FurthestLevelCompleted, level.LevelIdx);
        }
    }

    public string GetNextLevelName()
    {
        return levelsCompletion[FurthestLevelCompleted + 1].LevelName;
    }

    public string GetLevelName(int idx)
    {
        return levelsCompletion[idx].LevelName;
    }

    public LevelCompletion GetLevelCompletion(int levelIdx)
    {
        return levelsCompletion[levelIdx];
    }

    public void ClearData(IGameData data)
    {
        FurthestLevelCompleted = -1;
        FurthestSystemCompleted = -1;
        levelsCompletion.Clear();
        for (int idx = 0; idx < levelNames.Count; ++idx)
        {
            LevelCompletion completion = new();
            completion.Completed = false;
            completion.LevelIdx = idx;
            completion.LevelName = levelNames[idx];
            completion.GemsCollected = new List<bool> { false, false, false };
            levelsCompletion.Add(completion);
        }
    }

    public void CreateData(ref IGameData data)
    {
        GameData gameData = (GameData)data;
        gameData.LevelProgression.FurthestLevelCompleted = -1;
        gameData.LevelProgression.FurthestSystemCompleted = -1;
        gameData.LevelProgression.Levels.Clear();
        for (int idx = 0; idx < levelNames.Count; ++idx)
        {
            gameData.LevelProgression.Levels.Add(new LevelInfo());
            gameData.LevelProgression.Levels[idx].LevelName = levelNames[idx];
            gameData.LevelProgression.Levels[idx].LevelIndex = idx;
        }
    }

    public void LoadData(IGameData data)
    {
        GameData gameData = (GameData)data;
        FurthestLevelCompleted = gameData.LevelProgression.FurthestLevelCompleted;
        FurthestSystemCompleted = gameData.LevelProgression.FurthestSystemCompleted;

        levelsCompletion.Clear();
        foreach(var level in gameData.LevelProgression.Levels)
        {
            LevelCompletion completion = new();
            completion.Completed = level.LevelCompleted;
            completion.GemsCollected = level.GemsCollected;
            completion.LevelName = level.LevelName;
            completion.LevelIdx = level.LevelIndex;

            levelsCompletion.Add(completion);
        }
    }

    public void SaveData(ref IGameData data)
    {
        GameData gameData = (GameData)data;
        gameData.LevelProgression.FurthestLevelCompleted = FurthestLevelCompleted;
        gameData.LevelProgression.FurthestSystemCompleted = FurthestSystemCompleted;

        gameData.LevelProgression.Levels.Clear();
        foreach (var level in levelsCompletion)
        {
            LevelInfo levelInfo = new();
            levelInfo.LevelCompleted = level.Completed;
            levelInfo.GemsCollected = level.GemsCollected;
            levelInfo.LevelIndex = level.LevelIdx;
            levelInfo.LevelName = level.LevelName;
            gameData.LevelProgression.Levels.Add(levelInfo);
        }
    }
}
