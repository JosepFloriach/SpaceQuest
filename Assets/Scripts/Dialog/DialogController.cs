using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public Action DialogStarted;
    public Action DialogFinished;

    private List<DialogMessage> messages = new();
    private DialogSetup setup;
    private int currentMessageIdx;
    private bool isEnabled = false;

    public bool IsEnabled
    {
        get
        {
            return isEnabled;
        }
    }

    public string CurrentMessage
    {
        get
        {
            return messages[currentMessageIdx].Setup.Message;
        }
    }

    public Sprite CurrentAvatar
    {
        get
        {
            return messages[currentMessageIdx].Setup.Avatar;
        }
    }

    public bool CurrentNextButtonEnabled
    {
        get
        {
            return messages[currentMessageIdx].Setup.NextButtonEnabled;
        }
    }

    public bool CurrentPreviousButtonEnabled
    {
        get
        {
            return messages[currentMessageIdx].Setup.PrevButtonEnabled;
        }
    }

    public void OpenDialog(DialogSetup setup)
    {
        this.setup = setup;
        InitializeDialog(setup);

        isEnabled = true;
        currentMessageIdx = 0;
        messages[currentMessageIdx].Enable();
        SendDialogStartedEvent();        
    }

    public void NextMessageRequest()
    {
        NextMessage();
    }

    public void PreviousMessageRequest()
    {
        PreviousMessage();
    }

    public void CloseDialog()
    {
        SendDialogFinishedEvent();
        currentMessageIdx = 0;
        isEnabled = false;
    }

    private void PreviousMessage()
    { 
        if (currentMessageIdx > 0)
        {
            messages[currentMessageIdx].Disable();
            currentMessageIdx--;
            messages[currentMessageIdx].Enable();
        }
    }

    private void NextMessage()
    {
        if (currentMessageIdx == setup.Messages.Count - 1)
        {
            messages[currentMessageIdx].Disable();
            CloseDialog();
            DestroyDialog();
        }
        else
        {
            messages[currentMessageIdx].Disable();
            currentMessageIdx++;
            messages[currentMessageIdx].Enable();
        }
    }

    private void Update()
    {
        if (isEnabled)
        {
            messages[currentMessageIdx].Update(Time.deltaTime);
        }
    }

    private void InitializeDialog(DialogSetup setup)
    {
        foreach (var message in setup.Messages)
        {
            var dialog = new DialogMessage(message);
            messages.Add(dialog);
            dialog.MessageFinish += OnMessageFinish;
        }
    }

    private void DestroyDialog()
    {
        messages.Clear();
    }

    private void OnMessageFinish()
    {
        NextMessage();
    }

    private void SendDialogFinishedEvent()
    {
        DialogFinished?.Invoke();
    }

    private void SendDialogStartedEvent()
    {
        DialogStarted?.Invoke();
    }
}
