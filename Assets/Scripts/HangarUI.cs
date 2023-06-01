using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangarUI : MonoBehaviour
{
    [SerializeField] private Image spriteHolder;

    private HangarController hangarController;

    private void Awake()
    {
        hangarController = FindObjectOfType<HangarController>();
        hangarController.ShipUpdated += OnShipUpdated;
    }

    private void OnShipUpdated(object sender, CockpitSetup setup)
    {
        spriteHolder.sprite = setup.ShipPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
