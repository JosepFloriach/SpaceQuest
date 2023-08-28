using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrigger : MonoBehaviour
{
    private enum RotateDirection
    {
        Left,
        Right
    }

    [SerializeField] private RotateDirection direction;
    [SerializeField] private float seconds;
    
    private ShipSpawner shipSpawner;
    
    private void Awake()
    {
        shipSpawner = FindObjectOfType<ShipSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(RotateShip());
        }
    }

    private IEnumerator RotateShip()
    {
        float totalTime = 0.0f;
        while (totalTime < seconds)
        {
            totalTime += Time.deltaTime;
            if (direction == RotateDirection.Left)
            {
                shipSpawner.Ship.RotateLeft();
            }
            else
            {
                shipSpawner.Ship.RotateRight();
            }
            yield return null;
        }
        shipSpawner.Ship.ResetGyroscope();
    }
}
