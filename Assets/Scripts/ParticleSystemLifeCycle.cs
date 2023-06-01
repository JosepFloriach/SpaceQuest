using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemLifeCycle : MonoBehaviour
{
    public static event EventHandler<ParticleSystem> Birth;
    public static event EventHandler<ParticleSystem> Death;

    private new ParticleSystem particleSystem;
    private bool deathEventSent = false;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        SendBirthEvent();
    }

    private void Update()
    {
        if (!particleSystem.IsAlive() && !deathEventSent)
        {
            SendDeathEvent();
            deathEventSent = true;
        }
    }

    private void SendBirthEvent()
    {
        Birth?.Invoke(this, GetComponent<ParticleSystem>());
    }

    private void SendDeathEvent()
    {
        Death?.Invoke(this, GetComponent<ParticleSystem>());
    }
}
