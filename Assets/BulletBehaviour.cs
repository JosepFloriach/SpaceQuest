using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float speed;
    private Transform target;
    private float distanceThreshold;
    private GameObject destructionPrefab;

    public void SetDestructionPrefab(GameObject destructionPrefab)
    {
        this.destructionPrefab = destructionPrefab;
    }

    public void SetDistanceThreshold(float distanceThreshold)
    {
        this.distanceThreshold = distanceThreshold;
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
        Vector3 direction = target.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;

        if (direction.sqrMagnitude < distanceThreshold * distanceThreshold)
        {
            var explosion = GameObject.Instantiate(destructionPrefab, transform.parent);
            explosion.transform.position = target.transform.position;
            explosion.GetComponent<ParticleSystem>().Play();
            target.gameObject.SetActive(false);
            GameObject.Destroy(this.gameObject);
        }
    }
}
