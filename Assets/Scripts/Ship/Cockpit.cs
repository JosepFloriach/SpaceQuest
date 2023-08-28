using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhysicsBodyBehaviour))]
public class Cockpit : MonoBehaviour, IDestructible, IFreezable
{
    public event Action throttlingForward;
    public event Action throttlingBackwards;
    public event Action rotatingLeft;
    public event Action rotatingRight;

    [SerializeField] public CockpitSetup cockpitSetup;    
    [SerializeField] private bool godMode = false;
    [SerializeField] private AnimationCurve gravityFactorCurve;

    private Player player;
    private ShipSpawner shipSpawner;
    private SoundManager soundManager;

    private IPhysicsBody physicsBody;  
    private IForce1D verticalThruster;    
    private IForce1D gyroscope;
    private IShipDeposit fuelDeposit;
    private IShipDeposit quantumDeposit;
    private WarpEngine warpEngine;   

    private float diffTime;
    private bool allowForwardThruster = true;
    private bool allowBackwardThruster = true;
    private bool allowGyroscope = true;
    private bool allowWarpEngine = true;

    private bool infiniteFuelDeposit = false;
    private bool infiniteQuantumDeposit = false;

    public void Destroy()
    {
        player.Kill();
    }
    public void Freeze()
    {
        IsFrozen = true;
        ResetVerticalThruster();
        ResetGyroscope();
        ResetWarpEngine();
        PhysicsBody.Freeze();
    }

    public void Unfreeze()
    {
        IsFrozen = false;
        PhysicsBody.UnFreeze();
    }

    public IPhysicsBody PhysicsBody
    {
        get 
        {
            return physicsBody;
        }
    }

    public bool ThrustingForward
    {
        get;
        private set;
    }

    public bool ThrustingBackward
    {
        get;
        private set;
    }

    public bool TravelingBackward
    {
        get
        {
            return Vector2.Dot(transform.up, physicsBody.LinearVelocity.normalized) < 0;
        }
    }

    public bool WarpEngineActivated
    {
        get;
        private set;
    }

    public bool IsFrozen
    {
        get;
        private set;
    }

    public void EnableRearThruster(bool allow)
    {
        allowBackwardThruster = allow;
    }

    public void EnableFrontThruster(bool allow)
    {
        allowForwardThruster = allow;
    }

    public void AllowGyroscope(bool allow)
    {
        allowGyroscope = allow;
    }

    public void AllowWarpEngine(bool allow)
    {
        allowWarpEngine = allow;
    }

    public void SetInfiniteFuelDeposit(bool isInfinite)
    {
        infiniteFuelDeposit = isInfinite;
    }
    public void SetInfiniteQuantumDeposit(bool isInfinite)
    {
        infiniteQuantumDeposit = isInfinite;
    }
    public void RefillFuelDepositAbsoluteAmount(float amount)
    {
        fuelDeposit.RefillAbsoluteAmount(amount);
    }
    public void RefillQuantumDepositAbsoluteAmount(float amount)
    {
        quantumDeposit.RefillAbsoluteAmount(amount);
    }
    public void RefillFuelDepositPercentageAmount(float amount)
    {
        fuelDeposit.RefillPercentageAmount(amount);
    }
    public void RefillQuantumDepositPercentageAmount(float amount)
    {
        quantumDeposit.RefillPercentageAmount(amount);
    }
    public void SetFuelDeposit(float percentage)
    {
        fuelDeposit.SetPercentageAmount(percentage);
    }

    public void SetQuantumDeposit(float percentage)
    {
        quantumDeposit.SetPercentageAmount(percentage);
    }

    public float GetCurrentFuel()
    {
        return fuelDeposit.Current;
    }

    public float GetFuelCapacity()
    {
        return fuelDeposit.MaxCapacity;
    }

    public float GetCurrentQuantumEnergy()
    {
        return quantumDeposit.Current;
    }

    public float GetQuantumDepositCapacity()
    {
        return quantumDeposit.MaxCapacity;
    }

    public void TriggerGodMode()
    {
        godMode = !godMode;
    }

    public void SetGodMode(bool enabled)
    {
        godMode = enabled;
    }

    public void SetSpeed(float speed)
    {
        PhysicsBody.SetLinearSpeed(speed);
    }

    public void EnableWarpEngine()
    {
        if (!allowWarpEngine || !player.IsAlive || quantumDeposit.Current <= 0.0f)
        {
            return;
        }
        warpEngine.Enable();
        soundManager.PlaySound("WarpEngineEffect");
        WarpEngineActivated = true;
    }

    public void ResetWarpEngine()
    {
        warpEngine.Disable();
        soundManager.StopSound("WarpEngineEffect");
        WarpEngineActivated = false;
    }

    public void ThrustForward()
    {
        if (!allowForwardThruster || !player.IsAlive || fuelDeposit.Current <= 0.0f || IsFrozen)
        {
            return;
        }
        throttlingForward?.Invoke();
        verticalThruster.AddPositive(PhysicsBody, cockpitSetup.VerticalThrusterPower * cockpitSetup.VerticalThrusterPowerFactor, 0.01f);
        ThrustingForward = true;
        ThrustingBackward = false;
    }

    public void ThrustBackward()
    {
        if (!allowBackwardThruster || !player.IsAlive || fuelDeposit.Current <= 0.0f || IsFrozen)
        {
            return;
        }
        throttlingBackwards?.Invoke();
        verticalThruster.AddNegative(PhysicsBody, cockpitSetup.VerticalThrusterPower * cockpitSetup.VerticalThrusterPowerFactor, 0.01f);
        ThrustingForward = false;
        ThrustingBackward = true;
    }

    public void ResetGyroscope()
    {
        gyroscope.Reset(PhysicsBody);
    }

    public void ResetVerticalThruster()
    {
        verticalThruster.Reset(PhysicsBody);
        ThrustingBackward = false;
        ThrustingForward = false;        
    }

    public void RotateRight()
    {
        if (!allowGyroscope || !player.IsAlive)
        {
            return;
        }
        if (warpEngine.IsEnabled)
        {
            gyroscope.AddNegative(PhysicsBody, cockpitSetup.RotationThrusterPower * cockpitSetup.QuantumRotationPowerFactor, 0.01f);
        }
        else
        {
            float gravityFactor = 1.0f; // GetGravityRotationFactor();
            gyroscope.AddNegative(PhysicsBody, cockpitSetup.RotationThrusterPower * cockpitSetup.RotationPowerFactor * gravityFactor, 0.01f);
        }
        rotatingRight?.Invoke();
    }

    public void RotateLeft()
    {
        if (!allowGyroscope || !player.IsAlive)
        {
            return;
        }
        if (warpEngine.IsEnabled)
        {
            gyroscope.AddPositive(PhysicsBody, cockpitSetup.RotationThrusterPower * cockpitSetup.QuantumRotationPowerFactor, 0.01f);
        }
        else
        {
            float gravityFactor = 1.0f; // GetGravityRotationFactor();
            gyroscope.AddPositive(PhysicsBody, cockpitSetup.RotationThrusterPower * cockpitSetup.RotationPowerFactor * gravityFactor, 0.01f);
        }
        rotatingLeft?.Invoke();
    }

    public void StopRotation()
    {
        gyroscope.Reset(PhysicsBody);
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        shipSpawner = FindObjectOfType<ShipSpawner>();
        soundManager = FindObjectOfType<SoundManager>();

        verticalThruster = GetComponentInChildren<VerticalThruster>();        
        gyroscope = GetComponentInChildren<Gyroscope>();
        warpEngine = GetComponentInChildren<WarpEngine>();
        physicsBody = GetComponent<PhysicsBodyBehaviour>().PhysicsBody;

        ReferenceValidator.NotNull(player, shipSpawner, soundManager, verticalThruster, gyroscope, warpEngine, physicsBody);

        fuelDeposit = new ShipDeposit(cockpitSetup.MaxFuelCapacity * cockpitSetup.MaxFuelCapacityFactor);
        quantumDeposit = new ShipDeposit(cockpitSetup.MaxQuantumEnergy * cockpitSetup.MaxQuantumEnergyFactor);

        
    }

    private void Start()
    {
        fuelDeposit.RefillCompletely();
    }

    private void OnEnable()
    {
        player.PlayerKilled += OnPlayerKilled;
        player.PlayerWon += OnPlayerWon;
        shipSpawner.StartSpawn += OnStartSpawning;
        shipSpawner.ShipSpawned += OnShipSpawned;
    }

    private void OnDisable()
    {
        player.PlayerKilled -= OnPlayerKilled;
        player.PlayerWon -= OnPlayerWon;
        shipSpawner.StartSpawn -= OnStartSpawning;
        shipSpawner.ShipSpawned -= OnShipSpawned;
    }

    private void OnPlayerWon()
    {
        Freeze();
    }

    private void OnPlayerKilled()
    {
        Freeze();
    }

    private void OnStartSpawning()
    {
        PhysicsBody.SetLinearSpeed(0.0f);
        fuelDeposit.RefillCompletely();
        quantumDeposit.RefillCompletely();        
    }

    private void OnShipSpawned()
    {
        Unfreeze();
    }

    private void LateUpdate()
    {
        diffTime = Time.deltaTime;
        ClampLinearVelocity();
        UpdateDeposits();
    }

    private void ClampLinearVelocity()
    {      
        PhysicsBody.LinearVelocity = Vector3.ClampMagnitude(PhysicsBody.LinearVelocity, cockpitSetup.MaxSpeed * cockpitSetup.MaxSpeedFactor);
    }

    private void UpdateDeposits()
    {
        if (godMode)
        {
            return;
        }

        if (warpEngine.IsEnabled && !infiniteQuantumDeposit)
        {
            quantumDeposit.Consume(1000 * diffTime);
            
            if (GetCurrentQuantumEnergy() <= 0 && !godMode)
            {
                ResetWarpEngine();
            }                
        }

        if (verticalThruster.IsEnabled && !infiniteFuelDeposit)
        {
            fuelDeposit.Consume(verticalThruster.Direction.magnitude * diffTime);
            if (GetCurrentFuel() <= 0 && !godMode)
            {   
                ResetVerticalThruster();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();        
        if (interactable != null && collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            interactable.StartInteraction(player, PhysicsBody, transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            interactable.ContinueInteraction(player, PhysicsBody, transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null && collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            interactable.EndInteraction(player, PhysicsBody, transform);
        }
    }

    private float GetGravityRotationFactor()
    {
        IForce gravityForce = PhysicsBody.GetLinearForce("PlanetGravity");
        float gravityMagnitude = 0.0f;
        if (gravityForce != null)
        {
            gravityMagnitude = gravityForce.Direction.magnitude;
        }
        return gravityFactorCurve.Evaluate(gravityMagnitude);
    }
}
