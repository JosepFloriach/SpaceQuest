using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Cockpit : MonoBehaviour
{
    public event Action throttlingForward;
    public event Action throttlingBackwards;
    public event Action rotatingLeft;
    public event Action rotatingRight;

    [SerializeField] public CockpitSetup cockpitSetup;
    [SerializeField] private bool godMode = false;
    [SerializeField] private bool freeze = false;    
    
    private Player player;
    private ShipSpawner shipSpawner;
    
    private ShipComponent verticalThruster;    
    private ShipComponent gyroscope;
    private QuantumEngine quantumEngine;
    private FuelDeposit fuelDeposit;
    private QuantumEnergyDeposit quantumDeposit;

    private float diffTime;
    /*private float prevSpeed;

    private bool rotateRight;
    private bool rotateLeft;*/

    private bool throttlingBackwardsEnabled = true;
    private bool throttlingForwardEnabled = true;
    private bool gyroscopeEnabled = true;
    private bool quantumEngineEnabled = true;

    public PhysicsBody PhysicsBody
    {
        get;
        private set;
    }

    public void Freeze()
    {
        freeze = true;
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

    public bool TravelingBackwards
    {
        get;
        private set;
    }

    public void UnFreeze()
    {
        freeze = false;
    }

    public void EnableRearThruster(bool enable)
    {
        throttlingForwardEnabled = enable;
    }

    public void EnableFrontThruster(bool enable)
    {
        throttlingBackwardsEnabled = enable;
    }

    public void EnableGyroscope(bool enable)
    {
        gyroscopeEnabled = enable;
    }

    public void EnableQuantumEngine()
    {
        if (!quantumEngineEnabled)
        {
            return;
        }
        quantumEngine.Enable();
    }

    public void DisableQuantumEngine()
    {
        if (!quantumEngineEnabled)
        {
            return;
        }
        quantumEngine.Disable();
    }
        
    public void ThrustForward()
    {
        if (!throttlingForwardEnabled || fuelDeposit.CurrentFuel <= 0.0f || freeze)
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
        if (!throttlingBackwardsEnabled || fuelDeposit.CurrentFuel <= 0.0f || freeze)
        {
            return;
        }

        throttlingBackwards?.Invoke();
        verticalThruster.AddNegative(PhysicsBody, cockpitSetup.VerticalThrusterPower * cockpitSetup.VerticalThrusterPowerFactor, 0.01f);
        ThrustingForward = false;
        ThrustingBackward = true;
    }

    public void ResetVerticalThruster()
    {
        verticalThruster.Reset(PhysicsBody);
        ThrustingBackward = false;
        ThrustingForward = false;
    }

    public void RotateRight()
    {
        if (!gyroscopeEnabled)
        {
            return;
        }
        if (quantumEngine.IsEnabled)
        {
            gyroscope.AddNegative(PhysicsBody, cockpitSetup.RotationThrusterPower * cockpitSetup.QuantumRotationPowerFactor, 0.01f);
        }
        else
        {
            gyroscope.AddNegative(PhysicsBody, cockpitSetup.RotationThrusterPower * cockpitSetup.RotationPowerFactor, 0.01f);
        }
        rotatingRight?.Invoke();
        /*rotateRight = true;
        rotateLeft = false;*/
    }

    public void RotateLeft()
    {
        if (!gyroscopeEnabled)
        { 
            return;
        }
        if (quantumEngine.IsEnabled)
        {
            gyroscope.AddPositive(PhysicsBody, cockpitSetup.RotationThrusterPower * cockpitSetup.QuantumRotationPowerFactor, 0.01f);
        }
        else
        {
            gyroscope.AddPositive(PhysicsBody, cockpitSetup.RotationThrusterPower * cockpitSetup.RotationPowerFactor, 0.01f);
        }
        rotatingLeft?.Invoke();
        /*rotateRight = false;
        rotateLeft = true;*/
    }

    public void StopRotation()
    {
        gyroscope.Reset(PhysicsBody);
        /*rotateRight = false;
        rotateLeft = false;*/
    }

    public void RefillFuelDeposit(float amount)
    {
        fuelDeposit.RefillAmount(amount);
    }

    public void RefillQuantumEnergy(float amount)
    {
        quantumDeposit.RefillAmount(amount);
    }

    public float GetCurrentFuel()
    {
        return fuelDeposit.CurrentFuel;
    }

    public float GetFuelCapacity()
    {
        return fuelDeposit.MaxDeposit;
    }

    public float GetCurrentQuantumEnergy()
    {
        return quantumDeposit.CurrentEnergy;
    }

    public float GetQuantumEnergyCapacity()
    {
        return quantumDeposit.MaxDeposit;
    }

    public void TriggerGodMode()
    {
        godMode = !godMode;
    }

    public void SetSpeed(float speed)
    {
        if (godMode)
        {
            PhysicsBody.LinearVelocity = PhysicsBody.LinearVelocity.normalized * speed;
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        shipSpawner = FindObjectOfType<ShipSpawner>();

        verticalThruster = GetComponentInChildren<VerticalThruster>();        
        gyroscope = GetComponentInChildren<Gyroscope>();
        quantumEngine = GetComponentInChildren<QuantumEngine>();
        fuelDeposit = GetComponentInChildren<FuelDeposit>();
        quantumDeposit = GetComponentInChildren<QuantumEnergyDeposit>();

        PhysicsBody = new ShipPhysics(this, transform);
    }

    private void Start()
    {
        player.PlayerKilled += OnPlayerWon;
        player.PlayerWon += OnPlayerKilled;
        shipSpawner.StartSpawn += OnStartSpawning;
        shipSpawner.ShipSpawned += OnShipSpawned;
        fuelDeposit.RefillCompletely();
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
        PhysicsBody.ClearAllForces();
        fuelDeposit.RefillCompletely();
        quantumDeposit.RefillCompletely();
        PhysicsBody.ResetRotation();
    }

    private void OnShipSpawned()
    {
        UnFreeze();
    }

    private void LateUpdate()
    {
        if (freeze)
            return;

        //prevSpeed = PhysicsBody.LinearVelocity.magnitude;
        diffTime = Time.deltaTime;
       
        PhysicsBody.Update(diffTime);
        ClampLinearVelocity();
        UpdateDeposits();
        UpdateTravelingBackwardsTest();
    }

    private void ClampLinearVelocity()
    {      
        PhysicsBody.LinearVelocity = Vector3.ClampMagnitude(PhysicsBody.LinearVelocity, cockpitSetup.MaxSpeed * cockpitSetup.MaxSpeedFactor);
    }

    private void UpdateDeposits()
    {
        if (quantumEngine.IsEnabled)
        {
            quantumDeposit.Consume(1000 * diffTime);
            if (GetCurrentQuantumEnergy() <= 0 && !godMode)
            {
                quantumEngine.Disable();
            }                
        }

        if (verticalThruster.IsEnabled)
        {
            fuelDeposit.Consume(verticalThruster.Force.magnitude);
            if (GetCurrentFuel() <= 0 && !godMode)
            {   
                ResetVerticalThruster();
            }
        }
    }

    private void UpdateTravelingBackwardsTest()
    {
        TravelingBackwards = Vector2.Dot(PhysicsBody.LinearVelocity.normalized, transform.up) < 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<Interactable>();        
        if (interactable != null && collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            interactable.StartInteraction(player, PhysicsBody, transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<Interactable>();
        if (interactable != null && collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            interactable.ContinueInteraction(player, PhysicsBody, transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<Interactable>();
        if (interactable != null && collision.gameObject.layer == LayerMask.NameToLayer("Interactable"))
        {
            interactable.EndInteraction(player, PhysicsBody, transform);
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.green;
#endif
        if (verticalThruster != null)
        {
            /*Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + verticalThruster.Force);*/
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + PhysicsBody.LinearVelocity);

            Gizmos.color = Color.blue;
            foreach(Vector3 force in PhysicsBody.GetAllLinearForces())
            {
                Gizmos.DrawLine(transform.position, transform.position + force);
            }
        }

    }

}
