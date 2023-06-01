using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class EventListener
    {
        public GameObject externalObject;
        public MonoBehaviour externalScript;
        public string actionName;
        public int scriptIdx;
        public int actionIdx;

        private Delegate handler = null;
        private MonoBehaviour target = null;
        private EventInfo eventInfo = null;


        public void ConnectCallback(object type, Action callback)
        {           
            if (externalObject != null)
            {
                eventInfo = externalScript.GetType().GetEvent(actionName);
                handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, type, callback.Method);
                target = externalScript;
                eventInfo.AddEventHandler(target, handler);
            }           
        }

        public void DisconnectCallback(object type, Action callback)
        {
            eventInfo.RemoveEventHandler(target, handler);
            target = null;
            handler = null;
            eventInfo = null;
        }
    }
}
