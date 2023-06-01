using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class OnValueUpdate : UnityEvent<float> { }

/*[Serializable]
public class CircularSliderSetup
{
    [Range(0,1)]
    [SerializeField] public float Radius;
    [SerializeField] public GameObject Sprite;
    [SerializeField] public float Size;
    [Range(0, 360)]
    [SerializeField] public float Value;
    [SerializeField] public bool Interactable;
    [SerializeField] public OnValueUpdate callback;
}*/

public class NestedCircularSlider : MonoBehaviour, IDragHandler
{
    [SerializeField] public OnValueUpdate outCallback;

    public float value;

    private void Update()
    {
        ComputeSliderPositions(); 
    }

    private void ComputeSliderPositions()
    {
        outCallback?.Invoke(value);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 clickPosition = eventData.position;
        Vector2 center = transform.position;
        Vector2 direction = (clickPosition - center).normalized;

        value = (Mathf.Atan2(direction.y, direction.x)) * Mathf.Rad2Deg;
    }
}
