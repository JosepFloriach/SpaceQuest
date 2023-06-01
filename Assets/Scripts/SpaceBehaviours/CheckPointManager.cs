using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private List<CheckPoint> checkPoints;

    private ShipSpawner shipSpawner;
    private Cockpit cockpit;

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
        cockpit = FindObjectOfType<Cockpit>();
        shipSpawner = FindObjectOfType<ShipSpawner>();                
    }

    private void Start()
    {
        MoveToCurrentCheckPoint();
        shipSpawner.StartSpawn += OnStartSpawn;
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
        cockpit.transform.position = CurrentCheckPoint.transform.position;
    }

}
