using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPathNavigator : SplineNavigator
{
    private ShipSpawner shipSpawner;

    private void Awake()
    {
        shipSpawner = FindObjectOfType<ShipSpawner>();
        ReferenceValidator.NotNull(shipSpawner);
    }

    private void OnEnable()
    {
        shipSpawner.StartSpawn += OnShipSpawned;
    }

    private void OnDisable()
    {
        shipSpawner.StartSpawn -= OnShipSpawned;
    }

    private void OnShipSpawned()
    {
        objectToAnimate = FindObjectOfType<Cockpit>().gameObject;     
    }

    protected override void OnNavigationTick()
    {
        shipSpawner.Ship.PhysicsBody.ForceRotationToVelocity(false);
        shipSpawner.Ship.PhysicsBody.ForceRotationToInverseVelocity(false);
        shipSpawner.Ship.GetComponent<CockpitAnimations>().EnableForwardParticles();
    }

    /*protected override void OnTargetReached()
    {
        base.OnTargetReached();
        shipSpawner.Ship.GetComponent<CockpitAnimations>().DisableForwardParticles();
        shipSpawner.Ship.PhysicsBody.ForceRotationToVector3(false, (currentTargetPosition - prevPosition).normalized);
        shipSpawner.Ship.PhysicsBody.ForceRotationToVelocity(true);
    }*/
}
