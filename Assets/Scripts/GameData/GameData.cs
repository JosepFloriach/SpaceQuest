using jovetools.gameserialization;
using System;
using System.Collections.Generic;

[Serializable]
public class GameData : IGameData
{
    [Serializable]
    public class GameSetupData
    {
        public int Language;
        public int GraphicsQuality;
    }

    [Serializable]
    public class ShipsData
    {        
        public List<int> UnlockedShips = new();
        public List<int> Costs = new();
        public int CurrentShip;
    }

    [Serializable]
    public class CurrencyData
    {
        public int TotalAmountCollected;
        public int CurrentAmountAvailable;
        public int AmountSpent;
    }

    [Serializable]
    public class LevelProgressionData
    {
        public int FurthestSystemCompleted;
        public int FurthestLevelCompleted;
        public List<LevelInfo> Levels = new();

        [Serializable]
        public class LevelInfo
        {
            public int LevelIndex;
            public string LevelName;
            public bool LevelCompleted;
            public List<bool> GemsCollected;
   
            public LevelInfo()
            {
                GemsCollected = new List<bool> { false, false, false };
            }
        }
    }

    public GameSetupData GameSetup = new();
    public ShipsData Ships= new();
    public CurrencyData Currency = new();
    public LevelProgressionData LevelProgression = new(); 

    public bool ValidateData()
    {
        return CheckLevelSanity() && CheckUnlockedShipsSanity() && CheckCurrencySanity();
    }

    private bool CheckLevelSanity()
    {
        // Furthest level completed must be equal to the last level completed. It cannot be a level completed
        // with an index bigger than FursthestLevelCompleted. Also, a non-completed level cannot have any
        // gem collected.
        int furthestActualLevel = -1;
        foreach (var levelData in LevelProgression.Levels)
        {
            if (levelData.LevelCompleted)
            {
                furthestActualLevel = Math.Max(furthestActualLevel, levelData.LevelIndex);
            }
            else
            {
                foreach(bool gem in levelData.GemsCollected)
                {
                    if (gem)
                    {
                        return false;
                    }
                }
            }
        }

        return furthestActualLevel == LevelProgression.FurthestLevelCompleted;
    }

    private bool CheckUnlockedShipsSanity()
    {
        // The total cost of the ships unlocked must be less than the total gems collected.
        // Also the selected ship must be contained in the unlocked ships.
        int totalCost = 0;
        foreach (int ship in Ships.UnlockedShips)
        {
            totalCost += Ships.Costs[ship];
        }

        return Ships.UnlockedShips.Contains(Ships.CurrentShip) && totalCost == Currency.AmountSpent;
    }

    private bool CheckCurrencySanity()
    {
        // Total amount collected must be equal to all the gems collected through the levels.
        // Also the total amount collected must be equal to the current amount plus the spent amount.
        int collectedGemsThroughLevels = 0;
        foreach (var level in LevelProgression.Levels)
        {
            var gemsCollected = level.GemsCollected;
            for(int gemIdx = 0; gemIdx < gemsCollected.Count; ++gemIdx)
            {
                collectedGemsThroughLevels += gemsCollected[gemIdx]? 1: 0;
            }
        }

        return            
            collectedGemsThroughLevels == Currency.TotalAmountCollected &&
            Currency.TotalAmountCollected >= Currency.CurrentAmountAvailable &&
            Currency.AmountSpent <= Currency.TotalAmountCollected &&
            Currency.TotalAmountCollected == Currency.CurrentAmountAvailable + Currency.AmountSpent;
    }
}
