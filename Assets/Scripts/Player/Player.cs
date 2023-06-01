using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] private bool invencible;
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private float waitSecondsOnWinBeforeMovingMainMenu;
    
    private Cockpit cockpit;
    private Inventory inventory;

    public event Action PlayerKilled;
    public event Action PlayerWon;    

    private void Awake()
    {
        cockpit = GameObject.Instantiate(shipPrefab).GetComponent<Cockpit>();        
        inventory = GetComponent<Inventory>();
    }

    public int StarsCollected
    {
        get;
        private set;
    }

    public void AddStar()
    {
        StarsCollected++;
        cockpit.cockpitSetup.AvailableMechanicalPoints++;
    }

    public void TriggerInvencible()
    {
        invencible = !invencible;
    }

    public void Kill()
    {
        if (!invencible)
        {
            inventory.DropEverything();
            SendPlayerKilledEvent();
        }
    }

    public void Win()
    {
        PlayerWon?.Invoke();
        Invoke("MoveBackToMenu", waitSecondsOnWinBeforeMovingMainMenu);
    }

    private void SendPlayerKilledEvent()
    {
        PlayerKilled?.Invoke();
    }

    private void MoveBackToMenu()
    {
        var nameWithoutExtension = Path.GetFileNameWithoutExtension("Scenes/MainMenu");
        FindObjectOfType<SceneLoader>().SetScenePath(nameWithoutExtension);
        FindObjectOfType<SceneLoader>().LoadScene();
    }
}
