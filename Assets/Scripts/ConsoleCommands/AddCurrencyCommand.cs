using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCurrencyCommand : MonoBehaviour, IConsoleCommand
{
    private CurrencyController currencyController;

    private void Awake()
    {
        currencyController = FindObjectOfType<CurrencyController>();
    }

    public bool Execute(params string[] arguments)
    {
        int amount;
        if (!int.TryParse(arguments[1], out amount))
        {
            return false;
        }

        currencyController.AddCollectedGems(amount);
        return true;
    }

    public string GetCommandName()
    {
        return "AddCurrency";
    }

    public string GetHelp()
    {
        return "AddCurrency [value]: Adds the amount of currency specified by the first parameter.";
    }
}
