using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour, IFreezable
{

    public static event Action BulletHit;

    private float speed;
    private Transform target;
    private float distanceThreshold;
    private bool frozen;

    public void SetDistanceThreshold(float distanceThreshold)
    {
        this.distanceThreshold = distanceThreshold;
    }

    public void Freeze()
    {
        frozen = true;
    }

    public void Unfreeze()
    {
        frozen = false;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        Vector3 relativePos = target.position - transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90.0f, Vector3.forward);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    void Update()
    {
        if (frozen)
        {
            return;
        }

        Vector3 direction = target.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;

        if (direction.sqrMagnitude < distanceThreshold * distanceThreshold)
        {
            IDestructible destructible = target.GetComponent<IDestructible>();
            if (destructible != null)
            {
                destructible.Destroy();
            }           
            GameObject.Destroy(this.gameObject);
        }
    }
}
