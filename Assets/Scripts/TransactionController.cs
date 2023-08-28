using jovetools.gameserialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransactionController : MonoBehaviour
{
    private CurrencyController currencyController;
    private HangarController hangarController;
   
    private void Awake()
    {
        currencyController = FindObjectOfType<CurrencyController>();
        hangarController = FindObjectOfType<HangarController>();
        ReferenceValidator.NotNull(currencyController, hangarController);
    }

    public bool BuyShip(int shipIdx)
    {
        int currentGems = currencyController.CurrentGems;
        int shipCost = hangarController.GetShipAtIndex(shipIdx).GetComponent<Cockpit>().cockpitSetup.GemsCost;
        if (currentGems >= shipCost)
        {
            currencyController.SpendGems(shipCost);
            hangarController.UnlockShip(shipIdx);
            PersistanceManager<GameData>.Instance.SaveGame();
            return true;
        }
        return false;
    }
}
