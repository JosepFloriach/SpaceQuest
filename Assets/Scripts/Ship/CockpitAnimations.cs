using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Cockpit))]
[RequireComponent(typeof(Animator))]
public class CockpitAnimations : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathAnimationPrototype;
    [SerializeField] private GameObject speedEffect;
    [SerializeField] private List<ParticleSystem> forwardParticleSystems;
    [SerializeField] private List<ParticleSystem> backwardParticleSystems;

    private Player player;
    private ShipSpawner shipSpawner;
    private Cockpit cockpit;
    private Animator animator;
    private SoundManager soundManager;

    private bool killed = false;
    private bool spawned = true;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        shipSpawner = FindObjectOfType<ShipSpawner>();
        soundManager = FindObjectOfType<SoundManager>();
        cockpit = GetComponent<Cockpit>();
        animator = GetComponent<Animator>();

        ReferenceValidator.NotNull(
            player, 
            shipSpawner, 
            soundManager, 
            cockpit,
            animator, 
            deathAnimationPrototype, 
            speedEffect, 
            forwardParticleSystems, 
            backwardParticleSystems);
    }

    private void Start()
    {
        DisableSpeedEffect();
        DisableBackwardParticles();
        DisableForwardParticles();
    }

    private void OnEnable()
    {
        player.PlayerKilled += OnPlayerKilled;
        player.PlayerWon += OnPlayerWin;
        shipSpawner.StartSpawn += OnSpawnStarting;
        shipSpawner.ShipSpawned += OnSpawned;
    }

    private void OnDisable()
    {
        player.PlayerKilled -= OnPlayerKilled;
        player.PlayerWon -= OnPlayerWin;
        shipSpawner.StartSpawn -= OnSpawnStarting;
        shipSpawner.ShipSpawned -= OnSpawned;
    }

    private void Update()
    {
        if (killed || !spawned)
        {
            return;
        }

        if (cockpit.ThrustingForward)
        {
            animator.SetTrigger("VerticalForward");
        }
        else if (cockpit.ThrustingBackward)
        {
            animator.SetTrigger("VerticalBackward");
        }
        else
        {
            animator.SetTrigger("StopVertical");
        }

        if (cockpit.WarpEngineActivated)
        {
            soundManager.GetSound("Background").pitch = 0.75f;
        }
        else
        {
            soundManager.GetSound("Background").pitch = 1.0f;
        }

        float cockpitSpeed = cockpit.PhysicsBody.LinearSpeed;
        float normalizedSpeed = cockpitSpeed / (cockpit.cockpitSetup.MaxSpeed * cockpit.cockpitSetup.MaxSpeedFactor);
        animator.SetFloat("Speed", cockpitSpeed);
        animator.SetFloat("SpeedNormalized", normalizedSpeed);
        animator.SetBool("Frozen", cockpit.IsFrozen);
    }

    private void OnPlayerKilled()
    {
        animator.SetTrigger("Death");
        soundManager.PlaySound("Explosion");
        soundManager.StopSound("Background");
        killed = true;
        spawned = false;
    }

    private void OnPlayerWin()
    {
        animator.SetTrigger("Win");
        killed = false;
        spawned = false;
    }

    private void OnSpawnStarting()
    {
        killed = false;
        spawned = false;
    }

    private void OnSpawned()
    {
        spawned = true;
    }

    public void StartSpawn()
    {
        soundManager.PlaySound("Background");
        shipSpawner.StartSpawning();
    }

    public void FinishSpawn()
    {
        shipSpawner.FinishedSpawn();
    }

    public void DisableVolume()
    {
        GetComponentInChildren<Volume>().enabled = false;
    }

    public void EnableVolume()
    {
        GetComponentInChildren<Volume>().enabled = true;
    }

    public void PlayExplosionParticles()
    {
        ParticleSystem particles = GameObject.Instantiate(deathAnimationPrototype, transform.position + new Vector3(0.0f, 0.0f, 3.0f), transform.rotation, transform);
        particles.Play();
    }

    public void EnableSpeedEffect()
    {
        speedEffect.SetActive(true);
        if (cockpit.TravelingBackward)
        {
            speedEffect.transform.localEulerAngles =  new Vector3(270.0f, -90, 0.0f);
        }
        else
        {
            speedEffect.transform.localEulerAngles = new Vector3(90, -90, 0.0f);
        }
        soundManager.PlaySound("FastSpeed");
    }

    public void DisableSpeedEffect()
    {
        speedEffect.SetActive(false);
        soundManager.StopSound("FastSpeed");
    }

    public void HideSprite()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void ShowSprite()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    public void EnableForwardParticles()
    {
        foreach (var particleSystem in forwardParticleSystems)
        {
            particleSystem.enableEmission = true;
        }
    }

    public void DisableForwardParticles()
    {
        foreach (var particleSystem in forwardParticleSystems)
        {
            particleSystem.enableEmission = false;
        }
    }

    public void EnableBackwardParticles()
    {
        foreach (var particleSystem in backwardParticleSystems)
        {
            particleSystem.enableEmission = true;
        }
    }

    public void DisableBackwardParticles()
    {
        foreach (var particleSystem in backwardParticleSystems)
        {
            particleSystem.enableEmission = false;
        }
    }
}
