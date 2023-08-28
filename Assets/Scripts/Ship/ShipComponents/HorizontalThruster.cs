using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalThruster : MonoBehaviour, IForce1D
{
    [SerializeField] AnimationCurve ThrusterIncrementCurve;
    private float currentThrusterNormalized = 0.0f;
    private bool prevFrameLeft = false;
    private bool prevFrameRight = false;

    public bool IsEnabled { get; private set; }
    public bool IsEnabledPositive { get; }
    public bool IsEnabledNegative { get; }

    public Vector3 Direction
    {
        protected set;
        get;
    }
    public Vector3 DiffForce
    {
        private set;
        get;
    }

    public Vector3 PrevForce
    {
        private set;
        get;
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }

    public void AddPositive(IPhysicsBody cockpit, float maxPower, float diffPerThrust)
    {        
        if (prevFrameRight)
        {
            Reset(cockpit);
        }
        // Prev force should never get the same value than IForce. If maxPower is reached,
        // leave PrevForce untouched.
        if (Direction.magnitude < maxPower)
        {
            PrevForce = Direction;
        }
        float currentPower = GetPower(maxPower, diffPerThrust);
        Direction = -cockpit.Transform.right * currentPower;
        DiffForce = Direction - PrevForce;
        prevFrameLeft = true;
    }

    public void AddNegative(IPhysicsBody cockpit, float maxPower, float diffPerThrust)
    {        
        if (prevFrameLeft)
        {
            Reset(cockpit);
        }
        // Prev force should never get the same value than IForce. If maxPower is reached,
        // leave PrevForce untouched.
        if (Direction.magnitude < maxPower)
        {
            PrevForce = Direction;
        }
        float currentPower = GetPower(maxPower, diffPerThrust);
        Direction = cockpit.Transform.right * currentPower;
        DiffForce = Direction - PrevForce;
        prevFrameRight = true;
    }

    private float GetPower(float maxPower, float diffPerThrust)
    {
        currentThrusterNormalized = Mathf.Clamp(currentThrusterNormalized + (diffPerThrust /** Time.deltaTime*/), 0.0f, 1.0f);
        float currentPowerNormalized = ThrusterIncrementCurve.Evaluate(currentThrusterNormalized);
        float power = currentPowerNormalized * maxPower;
        return power;
    }

    public void Reset(IPhysicsBody cockpit)
    {
        Direction = Vector3.zero;
        PrevForce = Vector3.zero;
        DiffForce = Vector3.zero;
        currentThrusterNormalized = 0.0f;
        prevFrameLeft = false;
        prevFrameRight = false;
    }
}
