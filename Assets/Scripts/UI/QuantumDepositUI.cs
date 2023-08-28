using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuantumDepositUI : MonoBehaviour
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

    void Update()
    {
        if (shipSpawner.Ship != null && shipSpawner.Ship.GetQuantumDepositCapacity() != 0.0f)
        {
            slider.value = shipSpawner.Ship.GetCurrentQuantumEnergy() / shipSpawner.Ship.GetQuantumDepositCapacity();
        }
    }
}
