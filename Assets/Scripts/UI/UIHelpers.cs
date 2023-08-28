using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHelpers : MonoBehaviour
{
    [SerializeField] private Image uiMaskTop;
    [SerializeField] private Image uiMaskCenter;
    [SerializeField] private SpriteRenderer gravitySensor;
    [SerializeField] private Slider fuelSlider;
    [SerializeField] private Slider quantumEnergySlider;
    [SerializeField] private Slider timeSlider;

    public void HighlightGravitySensor(bool higlight)
    {
        if (higlight)
        {
            EnableUITutorialMask();
            gravitySensor.sortingOrder = 6;
        }
        else
        {
            DisableUITtutorialMask();
            gravitySensor.sortingOrder = 4;
        }
    }

    public void HighlightFuelSlider(bool highlight)
    {
        if (highlight)
        {
            EnableUITutorialMask();
            fuelSlider.GetComponent<Canvas>().sortingOrder = 6;
        }
        else
        {
            DisableUITtutorialMask();
            fuelSlider.GetComponent<Canvas>().sortingOrder = 4;
        }
    }

    public void HighlightQuantumEnergySlider(bool highlight)
    {
        if (highlight)
        {
            EnableUITutorialMask();
            quantumEnergySlider.GetComponent<Canvas>().sortingOrder = 6;
        }
        else
        {
            DisableUITtutorialMask();
            quantumEnergySlider.GetComponent<Canvas>().sortingOrder = 4;
        }
    }

    private void EnableUITutorialMask()
    {
        uiMaskTop.gameObject.SetActive(true);
        uiMaskCenter.gameObject.SetActive(true);
    }

    private void DisableUITtutorialMask()
    {
        uiMaskTop.gameObject.SetActive(false);
        uiMaskCenter.gameObject.SetActive(false);
    }
}
