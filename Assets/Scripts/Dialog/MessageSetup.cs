using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

[Serializable]
public enum NextMessageBehaviour
{
    ByTime,
    ByEvent
}

[Serializable]
public class MessageSetup
{
    public Sprite Avatar;
    public string Message;

    public bool NextButtonEnabled;
    public bool PrevButtonEnabled;

    public bool TriggerNextMessageByTime;
    public float Time;

    public List<UnityEvent> CallbacksOnOpen;
    public List<UnityEvent> CallbacksOnClose;
}
