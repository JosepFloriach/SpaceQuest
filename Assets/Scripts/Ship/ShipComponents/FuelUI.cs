using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private ShipSpawner shipSpawner;

    private void Awake()
    {
        shipSpawner = FindObjectOfType<ShipSpawner>();
        ReferenceValidator.NotNull(shipSpawner, slider);
    }

    private void Start()
    {
        slider.value = 1.0f;
    }

    private void Update()
    {
        if (shipSpawner.Ship != null && shipSpawner.Ship.GetFuelCapacity() != 0.0f)
        {
            float normalizedValue = shipSpawner.Ship.GetCurrentFuel() / shipSpawner.Ship.GetFuelCapacity();
            slider.value = normalizedValue;
        }
    }
}
