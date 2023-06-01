using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Cockpit))]
public class CockpitAnimations : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathAnimationPrototype;
    [SerializeField] private GameObject speedEffect;
    [SerializeField] private List<ParticleSystem> forwardParticleSystems;
    [SerializeField] private List<ParticleSystem> backwardParticleSystems;

    private Player player;
    private ShipSpawner shipSpawner;
    private Cockpit cockpit;

    private bool killed = false;
    private bool spawned = true;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        shipSpawner = GetComponent<ShipSpawner>();
        cockpit = GetComponent<Cockpit>();        
    }

    private void Start()
    {
        player.PlayerKilled += OnPlayerKilled;        
        player.PlayerWon += OnPlayerWin;
        shipSpawner.StartSpawn += OnSpawnStarting;
        shipSpawner.ShipSpawned += OnSpawned;
    }

    private void Update()
    {
        if (killed || !spawned)
        {
            DisableSpeedEffect();
            DisableBackwardParticles();
            DisableForwardParticles();
            return;
        }

        if (cockpit.PhysicsBody.LinearVelocity.magnitude > 25.0f)
        {
            EnableSpeedEffect();
        }
        else
        {
            DisableSpeedEffect();
        }

        if (cockpit.ThrustingForward)
        {
            EnableForwardParticles();
        }
        else if (cockpit.ThrustingBackward)
        {
            EnableBackwardParticles();
        }
        else
        {
            DisableForwardParticles();
            DisableBackwardParticles();
        }
    }

    private void OnPlayerKilled()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        PlayDeathAnimation();
        DisableSpeedEffect();
        killed = true;
        spawned = false;
    }

    private void OnPlayerWin()
    {
        DisableSpeedEffect();
        DisableVolume();
        GetComponent<Animator>().SetTrigger("Win");
        killed = false;
        spawned = false;
    }

    private void OnSpawnStarting()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        killed = false;
        spawned = false;
    }

    private void OnSpawned()
    {
        spawned = true;
        DisableVolume();
    }

    private void DisableVolume()
    {
        GetComponentInChildren<Volume>().enabled = false;
    }

    private void PlayDeathAnimation()
    {
        GetComponent<Animator>().SetTrigger("Death");
        ParticleSystem deathAnimation = GameObject.Instantiate(deathAnimationPrototype, transform.position + new Vector3(0.0f, 0.0f, 3.0f), transform.rotation, transform);
        deathAnimation.Play();
    }

    private void EnableSpeedEffect()
    {
        speedEffect.SetActive(true);
    }

    private void DisableSpeedEffect()
    {
        speedEffect.SetActive(false);
    }
    
    private void EnableForwardParticles()
    {
        foreach (var particleSystem in forwardParticleSystems)
        {
            particleSystem.enableEmission = true;
        }
    }

    private void DisableForwardParticles()
    {
        foreach (var particleSystem in forwardParticleSystems)
        {
            particleSystem.enableEmission = false;
        }
    }

    private void EnableBackwardParticles()
    {
        foreach (var particleSystem in backwardParticleSystems)
        {
            particleSystem.enableEmission = true;
        }
    }

    private void DisableBackwardParticles()
    {
        foreach (var particleSystem in backwardParticleSystems)
        {
            particleSystem.enableEmission = false;
        }
    }
}
