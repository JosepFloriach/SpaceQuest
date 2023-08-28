using jovetools.gameserialization;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HangarController : SingletonMonoBehaviour<HangarController>, ISerializable
{
    [SerializeField] private int defaultShip;
    [SerializeField] private List<GameObject> ships;
    
    private List<int> unlockedShips = new();
    private List<int> shipsCosts = new();
    public event Action<GameObject> SelectedShipUpdated;
    public event Action<GameObject> CurrentShipUpdated;

    private int currentShipIdx = 0;

    public List<GameObject> Ships
    {
        get
        {
            return ships;
        }
    }

    public GameObject SelectedShip
    {
        get
        {
            return ships[currentShipIdx];
        }
    }

    public int SelectedShipIdx
    {
        get
        {
            return currentShipIdx;
        }
    }

    public void OnDestroy()
    {
        PersistanceManager<GameData>.Instance.DeregisterSerializableObject(this);
    }

    public GameObject GetShipAtIndex(int shipIdx)
    {
        return ships[shipIdx];
    }

    public bool UnlockShip(int shipIdx)
    {
        unlockedShips.Add(shipIdx);
        return true;
    }

    public bool IsShipUnlocked(int shipIdx)
    {
        return unlockedShips.Contains(shipIdx);
    }

    public bool SelectShip(int shipIdx)
    {
        if(!unlockedShips.Contains(shipIdx))
        {
            return false;
        }
        currentShipIdx = shipIdx;
        PersistanceManager<GameData>.Instance.SaveGame();
        return true;
    }

    public void CreateData(ref IGameData data)
    {
        var gameData = (GameData)data;
        gameData.Ships.CurrentShip = defaultShip;
        shipsCosts.Clear();
        foreach (GameObject ship in ships)
        {
            int cost = ship.GetComponent<Cockpit>().cockpitSetup.GemsCost;
            shipsCosts.Add(cost);
        }
        gameData.Ships.Costs = shipsCosts;
        gameData.Ships.UnlockedShips = new List<int> { defaultShip };
    }

    public void LoadData(IGameData data)
    {
        var gameData = (GameData)data;
        currentShipIdx = gameData.Ships.CurrentShip;
        unlockedShips = gameData.Ships.UnlockedShips;
        shipsCosts = gameData.Ships.Costs;
    }

    public void SaveData(ref IGameData data)
    {
        var gameData = (GameData)data;
        gameData.Ships.CurrentShip = currentShipIdx;
        gameData.Ships.Costs = shipsCosts;
        gameData.Ships.UnlockedShips = unlockedShips;
    }

    public void ClearData(IGameData data)
    {
        currentShipIdx = defaultShip;
        unlockedShips = new List<int> { defaultShip };
    }
}
