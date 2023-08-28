using jovetools.gameserialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData.LevelProgressionData;
using static LevelProgressController;

public class LevelDataCollector : MonoBehaviour
{
    //[SerializeField] private LevelDataCollection levelDataCollection;

    private Player player;
    private LevelSetup levelSetup;
    private BonusGemsController bonusGemsController;
    private CurrencyController currencyController;
    private LevelProgressController levelProgressController;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        levelSetup = FindObjectOfType<LevelSetup>();
        levelProgressController = FindObjectOfType<LevelProgressController>();
        currencyController = FindObjectOfType<CurrencyController>();
        bonusGemsController = FindObjectOfType<BonusGemsController>();
        ReferenceValidator.NotNull(player, levelSetup, bonusGemsController);
    }

    private void OnEnable()
    {
        player.PlayerWon += OnPlayerWin;
    }

    private void OnDisable()
    {
        player.PlayerWon -= OnPlayerWin;
    }

    private void OnPlayerWin()
    {
        if (levelProgressController == null || currencyController == null)
        {
            // Fallback mecanism for starting level in unity editor.
            Debug.LogWarning("LevelProgressController or CurrencyController not found. Nothing will be saved");
            return;
        }

        LevelCompletion levelCompletion = new();
        levelCompletion.LevelIdx = levelSetup.LevelIndex;
        levelCompletion.LevelName = levelSetup.LevelName;
        levelCompletion.Completed = true;
        levelCompletion.GemsCollected = bonusGemsController.CollectedGems;
        levelProgressController.UpdateLevelProgress(levelCompletion);
        currencyController.AddCollectedGems(bonusGemsController.CollectedGemsForFirstTime);
        PersistanceManager<GameData>.Instance.SaveGame();
    }
}
