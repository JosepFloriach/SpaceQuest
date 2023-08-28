using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustBackwardTrigger : MonoBehaviour
{
    private enum ThrustDirection
    {
        Forward,
        Backward
    }

    [SerializeField] private ThrustDirection direction;
    [SerializeField] private float seconds;

    private ShipSpawner shipSpawner;
    private bool enable = false;
    private float currentSeconds;

    private void Awake()
    {
        shipSpawner = FindObjectOfType<ShipSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.tag == "Player")
        {
            StartCoroutine(Thrust());
        }*/
        enable = true;
    }

    private void FixedUpdate()
    {
        if (enable && currentSeconds < seconds)
        {
            if (direction == ThrustDirection.Forward)
            {
                shipSpawner.Ship.ThrustForward();
            }
            else
            {
                shipSpawner.Ship.ThrustBackward();
            }
            currentSeconds += Time.deltaTime;
        }
        else if (enable)
        {
            shipSpawner.Ship.ResetVerticalThruster();
            enable = false;
        }
    }

    private IEnumerator Thrust()
    {
        float totalTime = 0.0f;
        while (totalTime < seconds)
        {
            totalTime += Time.deltaTime;
            
            yield return null;
        }
        shipSpawner.Ship.ResetVerticalThruster();
    }
}
