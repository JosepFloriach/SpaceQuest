using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public event Action nextPressed;
    public event Action previousPressed;

    [SerializeField] private string OpenDialogAnimationTriggerName;
    [SerializeField] private string CloseDialogAnimationTriggerName;
    [SerializeField] private DialogController dialogController;
    [SerializeField] private TextMeshProUGUI textContainer;
    [SerializeField] private Image avatarContainer;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject prevButton;
    
    private void Awake()
    {
        if (dialogController == null)
        {
            dialogController = FindObjectOfType<DialogController>();
            if (dialogController == null)
            {
                throw new Exception("Required DialogController not found in the scene.");
            }
        }
    }

    private void Start()
    {
        dialogController.DialogStarted += TriggerDialogIntro;
        dialogController.DialogFinished += TriggerDialogOutro;
    }

    public void TriggerDialogIntro()
    {
        GetComponent<Animator>().SetTrigger(OpenDialogAnimationTriggerName);
    }

    public void TriggerDialogOutro()
    {
        GetComponent<Animator>().SetTrigger(CloseDialogAnimationTriggerName);
    }

    public void OnNextClicked()
    {
        dialogController.NextMessageRequest();
    }

    public void OnPreviousClicked()
    {
        dialogController.PreviousMessageRequest();
    }

    private void HideNextButton()
    {
        if (nextButton != null)
        {
            nextButton.SetActive(false);
        }
    }

    private void ShowNextButton()
    {
        if (nextButton != null)
        {
            nextButton.SetActive(true);
        }
    }

    private void HidePreviousButton()
    {
        if (prevButton != null)
        {
            prevButton.SetActive(false);
        }
    }

    private void ShowPreviousButton()
    {
        if (prevButton != null)
        {
            prevButton.SetActive(true);
        }
    }

    private void Update()
    {
        if (dialogController.IsEnabled)
        {
            if (textContainer != null)
            {
                textContainer.text = dialogController.CurrentMessage;
            }
            if (avatarContainer != null)
            {
                avatarContainer.sprite = dialogController.CurrentAvatar;
            }
            if (dialogController.CurrentNextButtonEnabled)
            {
                ShowNextButton();
            }
            else
            {
                HideNextButton();
            }
            if (dialogController.CurrentPreviousButtonEnabled)
            {
                ShowPreviousButton();
            }
            else
            {
                HidePreviousButton();
            }
        }
    }
}
