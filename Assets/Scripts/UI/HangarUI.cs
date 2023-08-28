using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class HangarUI : MonoBehaviour
{
    [SerializeField] private Image currentAvatar;
    [SerializeField] private Image previousAvatar;
    [SerializeField] private Image nextAvatar;
    [SerializeField] private Image button;
    [SerializeField] private Image gemImage;
    [SerializeField] private Image selected;

    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Color lockedAvatarColor;
    [SerializeField] private Color unlockedAvatarColor;
    [SerializeField] private ParameterUIController maxSpeedParameter;
    [SerializeField] private ParameterUIController accelerationParameter;
    [SerializeField] private ParameterUIController rotationPowerParameter;
    [SerializeField] private ParameterUIController fuelDepositParameter;
    [SerializeField] private ParameterUIController quantumDepositParameter;
    [SerializeField] private TextMeshProUGUI currencyText;

    private SoundManager soundManager;
    private HangarController hangarController;
    private TransactionController transactionController;

    private List<GameObject> ships;

    private int currentIdx;

    private void Awake()
    {
        hangarController = FindObjectOfType<HangarController>();
        transactionController = FindObjectOfType<TransactionController>();
        soundManager = FindObjectOfType<SoundManager>();
        ReferenceValidator.NotNull(
            hangarController, 
            transactionController, 
            soundManager, 
            currentAvatar,
            previousAvatar,
            nextAvatar,
            button,
            gemImage,
            selected,
            buyButton,
            selectButton,
            maxSpeedParameter,
            accelerationParameter,
            rotationPowerParameter,
            fuelDepositParameter,
            quantumDepositParameter,
            currencyText);
    }

    private void Start()
    {
        currentIdx = hangarController.SelectedShipIdx;
        ships = hangarController.Ships;
        UpdateUI();
    }

    public void OnNextClicked()
    {
        currentIdx = (currentIdx + 1) % ships.Count;        
        UpdateUI();
    }

    public void OnPreviousClicked()
    {
        currentIdx = currentIdx == 0 ? ships.Count - 1 : currentIdx - 1;
        UpdateUI();
    }

    public void OnBuySelectClicked()
    {
        if (hangarController.IsShipUnlocked(currentIdx))
        {
            hangarController.SelectShip(currentIdx);
        }
        else if (transactionController.BuyShip(currentIdx))
        {
            soundManager.PlaySound("BuyClick");
        }
        else
        {
            soundManager.PlaySound("WrongBuy");
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateAvatars();
        UpdateParameters();
        UpdateButton();
        UpdateSelectedImage();
        UpdateCost();
    }

    private void UpdateCost()
    {
        if (!hangarController.IsShipUnlocked(currentIdx))
        {
            gemImage.gameObject.SetActive(true);
            currencyText.gameObject.SetActive(true);
            currencyText.text = hangarController.GetShipAtIndex(currentIdx).GetComponent<Cockpit>().cockpitSetup.GemsCost.ToString();
        }
        else
        {
            gemImage.gameObject.SetActive(false);
            currencyText.gameObject.SetActive(false);
        }
    }

    private void UpdateAvatars()
    {
        int previousIdx = currentIdx == 0? ships.Count - 1 : currentIdx - 1;
        int nextIdx = currentIdx == ships.Count - 1? 0: currentIdx + 1;

        currentAvatar.sprite = hangarController.GetShipAtIndex(currentIdx).GetComponent<Cockpit>().cockpitSetup.HighResolutionSprite;
        nextAvatar.sprite = hangarController.GetShipAtIndex(nextIdx).GetComponent<Cockpit>().cockpitSetup.HighResolutionSprite;
        previousAvatar.sprite = hangarController.GetShipAtIndex(previousIdx).GetComponent<Cockpit>().cockpitSetup.HighResolutionSprite;

        previousAvatar.color = lockedAvatarColor;
        nextAvatar.color = lockedAvatarColor;

        if (hangarController.IsShipUnlocked(currentIdx))
        {
            currentAvatar.color = unlockedAvatarColor;
        }
        else
        {
            currentAvatar.color = lockedAvatarColor;

        }
    }

    private void UpdateParameters()
    {
        CockpitSetup setup = hangarController.GetShipAtIndex(currentIdx).GetComponent<Cockpit>().cockpitSetup;
        maxSpeedParameter.SetValue(setup.MaxSpeed);
        accelerationParameter.SetValue(setup.VerticalThrusterPower);
        rotationPowerParameter.SetValue(setup.RotationThrusterPower);
        fuelDepositParameter.SetValue(setup.MaxFuelCapacity);
        quantumDepositParameter.SetValue(setup.MaxQuantumEnergy);
    }

    private void UpdateButton()
    {
        if (hangarController.IsShipUnlocked(currentIdx))
        {
            selectButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            selectButton.SetActive(false);
            buyButton.SetActive(true);
        }
    }

    private void UpdateSelectedImage()
    {
        if (ships[currentIdx] == hangarController.SelectedShip)
        {
            selected.gameObject.SetActive(true);
        }
        else
        {
            selected.gameObject.SetActive(false);
        }
    }
}
