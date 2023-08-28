using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsUIController : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI countText;
    [SerializeField] LevelDataCollection progressData;

    private CurrencyController currencyController;

    private void Awake()
    {
        currencyController = FindObjectOfType<CurrencyController>();
        ReferenceValidator.NotNull(countText, progressData, currencyController);
    }

    private void Update()
    {
        // TODO: There is no need to put this in the update. We can subscribe to some event to get some performance.
        countText.text = currencyController.CurrentGems.ToString();
    }
}
