using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private List<CheckPoint> checkPoints;
    [SerializeField] private CheckPoint initialCheckPoint;

    private ShipSpawner shipSpawner;
    
    private CheckPoint currentCheckPoint;
    public CheckPoint CurrentCheckPoint
    {
        get
        {
            if (currentCheckPoint == null)
            {
                if (checkPoints.Count == 0)
                {
                    throw new MissingReferenceException("At least one checkpoint in the scene is required");
                }
                currentCheckPoint = checkPoints[0];
            }
            return currentCheckPoint;
        }
        set
        {
            currentCheckPoint = value;
        }
    }

    private void Awake()
    {
        shipSpawner = FindObjectOfType<ShipSpawner>();
        ReferenceValidator.NotNull(shipSpawner, checkPoints, initialCheckPoint);
    }

    private void Start()
    {
        CurrentCheckPoint = initialCheckPoint;
    }

    private void OnEnable()
    {
        shipSpawner.StartSpawn += OnStartSpawn;
    }

    private void OnDisable()
    {
        shipSpawner.StartSpawn -= OnStartSpawn;
    }

    public void AddCheckPoint(CheckPoint checkPoint)
    {
        checkPoints.Add(checkPoint);
    }

    private void OnStartSpawn()
    {
        MoveToCurrentCheckPoint();
    }

    private void MoveToCurrentCheckPoint()
    {
        shipSpawner.Ship.transform.position = CurrentCheckPoint.transform.position;
        shipSpawner.Ship.transform.rotation = CurrentCheckPoint.transform.rotation;
    }
}
