using jovetools.gameserialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionMapUIController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shipSprite;
    [SerializeField] private Color pathUnlockedColor;
    [SerializeField] private Color pathLockedColor;
    [SerializeField] private List<LevelSelector> levelSelectors;
 
    private HangarController hangarController;
    private LevelProgressController levelProgressController;
    private SceneLoader sceneLoader;
    private SplineNavigator splineNavigator;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        hangarController = FindObjectOfType<HangarController>();
        levelProgressController = FindObjectOfType<LevelProgressController>();
        splineNavigator = shipSprite.GetComponent<SplineNavigator>();
        ReferenceValidator.NotNull(
            sceneLoader,
            hangarController, 
            levelProgressController, 
            shipSprite, 
            levelSelectors);
    }

    private void Start()
    {
        shipSprite.sprite = hangarController.SelectedShip.GetComponentInChildren<SpriteRenderer>().sprite;
        UpdateUI();
    }

    private void OnEnable()
    {
        hangarController.SelectedShipUpdated += OnCurrentShipUpdated;
        PersistanceManager<GameData>.DataLoaded += UpdateUI;
        PersistanceManager<GameData>.DataReseted += OnReset;
    }

    private void OnDisable()
    {
        hangarController.SelectedShipUpdated -= OnCurrentShipUpdated;
        PersistanceManager<GameData>.DataLoaded -= UpdateUI;
        PersistanceManager<GameData>.DataReseted -= OnReset;
    }

    private void OnReset()
    {
        string nextLevelName = levelProgressController.GetNextLevelName();
        sceneLoader.SetScenePath(levelProgressController.GetLevelName(0));
        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateShipPosition();
        UpdateLevelSelectors();
    }

    private void OnCurrentShipUpdated(GameObject currentShip)
    {
        shipSprite.sprite = currentShip.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    private void UpdateLevelSelectors()
    {
        for (int idx = 0; idx < levelSelectors.Count; ++idx)
        {
            if (idx <= levelProgressController.FurthestLevelCompleted + 1)
            {
                levelSelectors[idx].IsActive = true;
            }
            else
            {
                levelSelectors[idx].IsActive = false;
            }
            levelSelectors[idx].DrawGemsGadget(levelProgressController.GetLevelCompletion(idx).GemsCollected);
        }
    }

    private void UpdateShipPosition()
    {
        LevelSelector levelSelector = levelSelectors[levelProgressController.FurthestLevelCompleted + 1];
        splineNavigator.SetInstantPosition(levelSelector.NormalizedSplinePosition);
    }   
}
