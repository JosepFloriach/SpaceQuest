using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : InteractableBase
{
    [SerializeField] private List<GameObject> ObjectsToDestroy;
    [SerializeField] private List<GameObject> ExplosionsPrefabs;
    [SerializeField] private bool DisableOnPickup;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float distanceThreshold;
  
    public override void StartInteraction(Player player, PhysicsBody cockpit, Transform transform)
    {
        base.StartInteraction(player, cockpit, transform);
        Shoot();
        if (DisableOnPickup)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Shoot()
    {
        foreach (var go in ObjectsToDestroy)
        {
            var bulletGO = GameObject.Instantiate(bulletPrefab);
            bulletGO.transform.position = transform.position;
            var bullet = bulletGO.GetComponent<BulletBehaviour>();
            bullet.SetTarget(go.transform);
            bullet.SetSpeed(speed);
            bullet.SetDistanceThreshold(distanceThreshold);
            bullet.SetDestructionPrefab(ExplosionsPrefabs[0]);
        }
    }

}
