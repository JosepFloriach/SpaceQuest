using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetter : MonoBehaviour
{
    private ShipSpawner shipSpawner;
    [SerializeField] private GameObject levelParent;
    [SerializeField] private List<GameObject> OtherReseteables;

    private void Awake()
    {
        shipSpawner = FindObjectOfType<ShipSpawner>();
        ReferenceValidator.NotNull(shipSpawner, levelParent, OtherReseteables);
    }

    private void OnEnable()
    {
        shipSpawner.ShipSpawned += OnShipSpawned;
    }

    private void OnDisable()
    {
        shipSpawner.ShipSpawned -= OnShipSpawned;
    }

    private void Reset()
    {
        foreach(var obj in levelParent.GetComponentsInChildren<IReseteable>(true))
        {
            obj.Reset();
        }

        foreach (var go in OtherReseteables)
        {
            IReseteable reseteable = go.GetComponent<IReseteable>();
            if (reseteable != null)
            {
                reseteable.Reset();
            }
        }
    }

    private void OnShipSpawned()
    {
        Reset();
    }
}
