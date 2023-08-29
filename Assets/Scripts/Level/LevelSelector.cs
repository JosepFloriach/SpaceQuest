using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private Color colorActiveGem;
    [SerializeField] private Color colorInactiveGem;
    [SerializeField] private Color colorInactivePlanet;
    [SerializeField] private Color colorActivePlanet;
    [SerializeField] private Color colorShipOverPlanet;
    [SerializeField] private GameObject gems;
    [SerializeField] private Button button;
    [SerializeField] private Image planetImage;
    [SerializeField] private float normalizedSplinePosition;

    private LevelProgressController levelProgressController;
    private SceneLoader sceneLoader;
    private SplineNavigator splineNavigator;
    private bool active;

    public float NormalizedSplinePosition
    {
        get
        {
            return normalizedSplinePosition;
        }
    }

    public bool IsActive
    {
        get
        {
            return active;
        }
        set
        {
            active = value;
            planetImage.color = active ? colorActivePlanet : colorInactivePlanet;
            button.interactable = active;
        }
    }

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        splineNavigator = FindObjectOfType<SplineNavigator>();
        levelProgressController = FindObjectOfType<LevelProgressController>();
        ReferenceValidator.NotNull(gems, button, planetImage, levelProgressController, sceneLoader, splineNavigator);
    }

    private void Start()
    {
        string nextLevelName = levelProgressController.GetNextLevelName();
        sceneLoader.SetScenePath(nextLevelName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponentInChildren<Image>().color = colorShipOverPlanet;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GetComponentInChildren<Image>().color = colorActivePlanet;
    }

    public void DrawGemsGadget(List<bool> gemsCollected)
    {
        var gemsObjs = gems.GetComponentsInChildren<SpriteRenderer>();
        for (int idx = 0; idx < gemsCollected.Count; ++idx)
        {
            gemsObjs[idx].GetComponent<SpriteRenderer>().color = gemsCollected[idx]? colorActiveGem: colorInactiveGem;            
        }
    }

    public void OnClick()
    {
        sceneLoader.SetScenePath(levelName);
        splineNavigator.SetTarget(normalizedSplinePosition);
        splineNavigator.Navigate();
    }
}
