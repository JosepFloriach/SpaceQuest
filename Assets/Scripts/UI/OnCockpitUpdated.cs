using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCockpitUpdated : MonoBehaviour
{
    [SerializeField] private Slider throttleUI;
    [SerializeField] private NestedCircularSlider joystickUI;

    //private CockpitController cockpitController;

    private void Awake()
    {
        //cockpitController = FindObjectOfType<CockpitController>();
    }

    private void Start()
    {
        //throttleUI.value = cockpitController.Throttle;
        //joystickUI.value = cockpitController.Direction;

        //cockpitController.ThrottleUpdated += SetThrottleUI;
        //cockpitController.DirectionUpdated += SetDirectionUI;
    }
    
    private void SetDirectionUI(float val)
    {
        joystickUI.value = val;
    }

    private void SetThrottleUI(float val)
    {
        throttleUI.value = val;
    }
}
