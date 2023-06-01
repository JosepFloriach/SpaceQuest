using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private bool infiniteTime;
    [SerializeField] private float maxTimeToCompleteTheLevel;
    
    Player player;
    ShipSpawner shipSpawner;
    Cockpit cockpit;
    bool running;
    bool ready;


    public bool InfiniteTime
    {
        get
        {
            return infiniteTime;
        }
        set
        {
            infiniteTime = value;
        }
    }
    
    public float CurrentTime
    {
        get; private set;
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        cockpit = FindObjectOfType<Cockpit>();
        shipSpawner = FindObjectOfType<ShipSpawner>();
    }

    private void Start()
    {        
        cockpit.throttlingForward += InitTimer;
        cockpit.throttlingBackwards += InitTimer;
        shipSpawner.ShipSpawned += Ready;
        player.PlayerKilled += Stop;
        CurrentTime = maxTimeToCompleteTheLevel;
        Ready();
    }

    private void Ready()
    {
        CurrentTime = maxTimeToCompleteTheLevel;
        ready = true;
    }

    private void Stop()
    {
        running = false;
        ready = false;
    }

    public void InitTimer()
    {
        if (ready && !running)
        {
            running = true;
        }
    }

    void Update()
    {
        if (!infiniteTime)
        {
            if (ready && running)
            {
                CurrentTime -= Time.deltaTime;
                if (CurrentTime <= 0.0f)
                {
                    player.Kill();
                    Stop();
                }
            }            
        }
    }
}
