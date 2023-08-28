using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField] private List<Cockpit> ships;
    //[SerializeField] private LevelDataCollection dataCollection;
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private bool spawnOnStart;
    [SerializeField] private GameObject levelParent;

    private HangarController hangarController;

    public Cockpit Ship
    {
        get;
        private set;
    }

    public event Action ShipSpawned;
    public event Action StartSpawn;

    private void Awake()
    {
        hangarController = FindObjectOfType<HangarController>();
        ReferenceValidator.NotNull(hangarController, ships, levelParent);
    }

    private void Start()
    {
        if (shipPrefab == null)
        {
            shipPrefab = hangarController.SelectedShip;
            if (shipPrefab == null)
            {
                throw new Exception("Requested ship not found");
            }
        }

        if (spawnOnStart)
        {
            InstantiateShip();
        }
    }

    public void InstantiateShip()
    {
        if (Ship != null)
        {
            throw new Exception("A ship is already instantiated");
        }
        Ship = GameObject.Instantiate(shipPrefab).GetComponent<Cockpit>();
        Ship.GetComponent<Animator>().SetTrigger("Spawn");
        Ship.transform.parent = levelParent.transform;
    }

    public void StartSpawning()
    {        
        StartSpawn?.Invoke();
    }

    public void FinishedSpawn()
    {
        ShipSpawned?.Invoke();
    }
}
