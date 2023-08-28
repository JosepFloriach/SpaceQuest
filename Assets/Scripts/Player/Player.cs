using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private bool invencible;
    [SerializeField] private float waitSecondsOnWinBeforeMovingMainMenu;

    private ShipSpawner shipSpawner;
    private Inventory inventory;

    public event Action PlayerKilled;
    public event Action PlayerWon;    

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        shipSpawner = GetComponent<ShipSpawner>();
    }

    private void Start()
    {        
        IsAlive = true;
    }

    private void OnEnable()
    {
        shipSpawner.ShipSpawned += OnFinishSpawn;
    }

    private void OnDisable()
    {
        shipSpawner.ShipSpawned -= OnFinishSpawn;
    }

    public bool LevelComplete
    {
        get;
        private set;
    }

    public bool IsAlive
    {
        get;
        private set;
    }

    public void TriggerInvencible()
    {
        invencible = !invencible;
    }

    public void Kill()
    {
        if (!invencible)
        {
            IsAlive = false;
            inventory.DropEverything();
            DialogController.GetInstance().CloseDialog();
            SendPlayerKilledEvent();            
        }
    }

    public void Win()
    {
        LevelComplete = true;
        PlayerWon?.Invoke();
        Invoke("MoveBackToMenu", waitSecondsOnWinBeforeMovingMainMenu);
    }

    private void SendPlayerKilledEvent()
    {
        PlayerKilled?.Invoke();
    }

    private void MoveBackToMenu()
    {
        FindObjectOfType<SceneLoader>().LoadMainMenu();
    }

    private void OnFinishSpawn()
    {
        IsAlive = true;
    }
}
