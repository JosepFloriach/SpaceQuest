using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public event Action ShipSpawned;
    public event Action StartSpawn;

    public void StartSpawning()
    {        
        StartSpawn?.Invoke();
    }

    public void FinishedSpawn()
    {
        ShipSpawned?.Invoke();
    }
}
