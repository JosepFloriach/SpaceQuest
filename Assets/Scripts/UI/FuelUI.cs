using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    private Cockpit cockpit;

    void Awake()
    {
        cockpit = FindObjectOfType<Cockpit>();
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = cockpit.GetCurrentFuel();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = cockpit.GetCurrentFuel() / cockpit.GetFuelCapacity();
    }
}
