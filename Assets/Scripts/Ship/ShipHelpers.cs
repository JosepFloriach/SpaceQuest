using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHelpers : MonoBehaviour
{
    private ShipSpawner shipSpawner;
    private KeyboardControls keyboardControls;

    private void Awake()
    {
        shipSpawner = FindObjectOfType<ShipSpawner>();
        keyboardControls = FindObjectOfType<KeyboardControls>();
        ReferenceValidator.NotNull(shipSpawner, keyboardControls);
    }

    public void SetFuel(float percentage)
    {
        shipSpawner.Ship.SetFuelDeposit(percentage);
    }

    public void SetQuantumEnergy(float percentage)
    {
        shipSpawner.Ship.SetQuantumDeposit(percentage);
    }

    public void SetGodMode(bool enabled)
    {
        shipSpawner.Ship.SetGodMode(enabled);
    }

    public void SetInfiniteQuantumDeposit(bool isInfinite)
    {
        shipSpawner.Ship.SetInfiniteQuantumDeposit(isInfinite);
    }

    public void SetInfiniteFuelDeposit(bool isInfinite)
    {
        shipSpawner.Ship.SetInfiniteFuelDeposit(isInfinite);
    }

    public void SetShipSpeed(float speed)
    {
        shipSpawner.Ship.SetSpeed(speed);
    }

    public void FreezeShip()
    {
        shipSpawner.Ship.Freeze();
    }

    public void SetRotationToVelocity()
    {
        shipSpawner.Ship.PhysicsBody.ForceRotationToVelocity(true);
    }

    public void ContinuePathTraveling(SplineNavigator pathNavigator)
    {
        pathNavigator.Navigate();
    }

    public void StopPathTraveling(SplineNavigator pathNavigator)
    {
        pathNavigator.Pause();
    }

    public void DisableKeyboardControls()
    {
        keyboardControls.Enable = false;
    }

    public void EnableKeyboardControls()
    {
        keyboardControls.Enable = true;
    }

    public void SetShipDirectionTangentialToPlanet(bool right)
    {
        Cockpit ship = shipSpawner.Ship;
        Vector3 centerPlanetPosition = ship.PhysicsBody.GetLinearForce("PlanetGravity").GetObject().transform.position;
        Vector3 shipPosition = ship.transform.position;
        Vector3 directionToPlanet = shipPosition - centerPlanetPosition;
        Vector3 tangentVector = new Vector3(directionToPlanet.y, -directionToPlanet.x).normalized;
        ship.PhysicsBody.LinearVelocity = tangentVector;
        ship.PhysicsBody.ForceRotationToVelocity(true);
    }

    public void FreezeOnFinishedSpawn()
    {
        shipSpawner.ShipSpawned += OnFinishedSpawn;
    }

    private void OnFinishedSpawn()
    {
        FreezeShip();
        shipSpawner.ShipSpawned -= OnFinishedSpawn;
    }

    public void UnFreezeShip()
    {
        shipSpawner.Ship.Unfreeze();
    }

    public void StopShip()
    {
        shipSpawner.Ship.PhysicsBody.ClearAllForces();
    }

    public void SpawnShip()
    {
        shipSpawner.InstantiateShip();
    }

    public void EnableGyroscope()
    {
        shipSpawner.Ship.AllowGyroscope(true);
    }

    public void DisableGyroscope()
    {
        shipSpawner.Ship.AllowGyroscope(false);
    }

    public void EnableFrontThruster()
    {
        shipSpawner.Ship.EnableFrontThruster(true);
    }

    public void DisableFrontThruster()
    {
        shipSpawner.Ship.EnableFrontThruster(false);
    }

    public void EnableRearThruster()
    {
        shipSpawner.Ship.EnableRearThruster(true);
    }

    public void DisableRearThruster()
    {
        shipSpawner.Ship.EnableRearThruster(false);
    }
    public void EnableWarpEngine()
    {
        shipSpawner.Ship.AllowWarpEngine(true);
    }

    public void DisableWarpEngine()
    {
        shipSpawner.Ship.AllowWarpEngine(false);
    }
}
