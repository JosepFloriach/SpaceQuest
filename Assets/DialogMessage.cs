using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utils;

public class DialogMessage
{
    public Action MessageStart;
    public Action MessageFinish;
    
    private float currentTime = 0.0f;

    public MessageSetup Setup
    {
        get;
        private set;
    }

    public DialogMessage(MessageSetup setup)
    {
        this.Setup = setup;
    }

    public void Enable()
    {
        InvokeOnOpenCallbacks();
        currentTime = 0.0f;
    }

    public void Disable()
    {
        InvokeOnCloseCallbacks();
    }

    public void Update(float diffTime)
    {
        if (Setup.TriggerNextMessageByTime)
        {
            currentTime += diffTime;
            if (currentTime >= Setup.Time)
            {
                SendMessageFinishEvent();
            }
        }
    }

    private void InvokeOnOpenCallbacks()
    {
        if (Setup.CallbacksOnOpen.Count == 0)
        {
            return;
        }

        foreach (var callback in Setup.CallbacksOnOpen)
        {
            callback.Invoke();
        }  
    }

    private void InvokeOnCloseCallbacks()
    {
        if (Setup.CallbacksOnClose.Count == 0)
        {
            return;
        }
            
        foreach (var callback in Setup.CallbacksOnClose)
        {
            callback.Invoke();
        }
    }

    private void SendMessageFinishEvent()
    {
        MessageFinish?.Invoke();
    }
}
