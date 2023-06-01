using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetter : MonoBehaviour
{
    private ShipSpawner shipSpawner;

    private void Awake()
    {
        shipSpawner = FindObjectOfType<ShipSpawner>();
    }

    private void Start()
    {
        shipSpawner.ShipSpawned += OnShipSpawned;
    }

    private void Reset()
    {
        foreach(var obj in GetComponentsInChildren<BasePickup>(true))
        {
            obj.gameObject.SetActive(true);
        }
        foreach (var obj in GetComponentsInChildren<InteractableBase>(true))
        {
            obj.gameObject.SetActive(true);
        }
    }

    private void OnShipSpawned()
    {
        Reset();
    }
}
