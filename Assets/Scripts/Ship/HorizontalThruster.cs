using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalThruster : MonoBehaviour, ShipComponent
{
    [SerializeField] AnimationCurve ThrusterIncrementCurve;
    private float currentThrusterNormalized = 0.0f;
    private bool prevFrameLeft = false;
    private bool prevFrameRight = false;

    public bool IsEnabled { get; private set; }
    public bool IsEnabledPositive { get; }
    public bool IsEnabledNegative { get; }

    public Vector3 Force 
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

    public void AddPositive(PhysicsBody cockpit, float maxPower, float diffPerThrust)
    {        
        if (prevFrameRight)
        {
            Reset(cockpit);
        }
        // Prev force should never get the same value than Force. If maxPower is reached,
        // leave PrevForce untouched.
        if (Force.magnitude < maxPower)
        {
            PrevForce = Force;
        }
        float currentPower = GetPower(maxPower, diffPerThrust);
        Force = -cockpit.Transform.right * currentPower;
        DiffForce = Force - PrevForce;
        prevFrameLeft = true;
    }

    public void AddNegative(PhysicsBody cockpit, float maxPower, float diffPerThrust)
    {        
        if (prevFrameLeft)
        {
            Reset(cockpit);
        }
        // Prev force should never get the same value than Force. If maxPower is reached,
        // leave PrevForce untouched.
        if (Force.magnitude < maxPower)
        {
            PrevForce = Force;
        }
        float currentPower = GetPower(maxPower, diffPerThrust);
        Force = cockpit.Transform.right * currentPower;
        DiffForce = Force - PrevForce;
        prevFrameRight = true;
    }

    private float GetPower(float maxPower, float diffPerThrust)
    {
        currentThrusterNormalized = Mathf.Clamp(currentThrusterNormalized + (diffPerThrust /** Time.deltaTime*/), 0.0f, 1.0f);
        float currentPowerNormalized = ThrusterIncrementCurve.Evaluate(currentThrusterNormalized);
        float power = currentPowerNormalized * maxPower;
        return power;
    }

    public void Reset(PhysicsBody cockpit)
    {
        Force = Vector3.zero;
        PrevForce = Vector3.zero;
        DiffForce = Vector3.zero;
        currentThrusterNormalized = 0.0f;
        prevFrameLeft = false;
        prevFrameRight = false;
    }
}
