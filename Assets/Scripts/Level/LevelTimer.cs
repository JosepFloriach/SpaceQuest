using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour, IFreezable
{
    [SerializeField] private bool infiniteTime;
    [SerializeField] private float maxTimeToCompleteTheLevel;
    [SerializeField] private AnimationCurve speedFactor;
    [SerializeField] private AnimationCurve gravityFactor;

    private Player player;
    private ShipSpawner shipSpawner;

    private bool running;
    private bool ready;
    private bool init;

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
    
    public float MaxTime
    {
        get { return maxTimeToCompleteTheLevel; }
        private set { maxTimeToCompleteTheLevel = value; }
    }

    public float CurrentTime
    {
        get; private set;
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        shipSpawner = FindObjectOfType<ShipSpawner>();
        ReferenceValidator.NotNull(player, shipSpawner, speedFactor, gravityFactor);
    }

    private void Start()
    {
        CurrentTime = maxTimeToCompleteTheLevel;
        Ready();
        init = false;
    }

    private void OnEnable()
    {
        shipSpawner.ShipSpawned += Ready;
        player.PlayerWon += Stop;
        player.PlayerKilled += Stop;
    }

    private void OnDisable()
    {
        shipSpawner.ShipSpawned -= Ready;
        player.PlayerWon -= Stop;
        player.PlayerKilled -= Stop;
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
        if (shipSpawner.Ship == null)
        {
            return;
        }

        if (!init)
        {
            init = true;
            shipSpawner.Ship.throttlingForward += InitTimer;
            shipSpawner.Ship.throttlingBackwards += InitTimer;
        }

        if (!infiniteTime)
        {
            if (ready && running)
            {                
                CurrentTime -= (Time.deltaTime * speedFactor.Evaluate(shipSpawner.Ship.PhysicsBody.LinearSpeed));
                if (CurrentTime <= 0.0f)
                {
                    player.Kill();
                    Stop();
                }
            }            
        }
    }

    public void Freeze()
    {
        running = false;
    }

    public void Unfreeze()
    {
        running = true;
    }
}
