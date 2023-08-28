using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class FunctionTriggererOnCollider2DEvent : MonoBehaviour
{
    private enum EventType
    {
        OnTriggerEnter,
        OnTriggerExit
    };

    [SerializeField] private EventType eventType;
    [SerializeField] private UnityEvent<int> methodToInvoke;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (eventType == EventType.OnTriggerEnter && other.tag == "Player")
        {
            methodToInvoke?.Invoke(3);
        }
    }
}
