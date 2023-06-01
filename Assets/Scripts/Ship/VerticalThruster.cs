using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalThruster : MonoBehaviour, ShipComponent
{
    [SerializeField] AnimationCurve ThrusterIncrementCurve;

    private float currentThrusterNormalizedForward = 0.0f;
    private float currentThrusterNormalizedBackward = 0.0f;

    public bool IsEnabled { get; private set; }
    public bool IsEnabledPositive { get; private set; }
    public bool IsEnabledNegative { get; private set; }

    private Vector3 PrevForce;

    public Vector3 Force 
    {
        protected set;
        get;
    }

    public void AddPositive(PhysicsBody cockpit, float maxPower, float diffPerThrust)
    {
        IsEnabled = true;
        IsEnabledPositive = true;
        IsEnabledNegative = false;

        float currentPower = GetPower(maxPower, diffPerThrust, true);
        Force = cockpit.Transform.up * currentPower;
        cockpit.AddLinearForce("VerticalThruster", Force);
        currentThrusterNormalizedBackward = 0.0f;
    }

    public void AddNegative(PhysicsBody cockpit, float maxPower, float diffPerThrust)
    {
        IsEnabled = true;
        IsEnabledPositive = false;
        IsEnabledNegative = true;

        float currentPower = GetPower(maxPower, diffPerThrust, false);
        Force = (-cockpit.Transform.up * currentPower);
        cockpit.AddLinearForce("VerticalThruster", Force);
        currentThrusterNormalizedForward = 0.0f;
        PrevForce = Force;
    }

    private float GetPower(float maxPower, float diffPerThrust, bool forward)
    {
        float currentPowerNormalized = 0.0f;
        if (forward)
        {
            currentThrusterNormalizedForward = Mathf.Clamp(currentThrusterNormalizedForward + diffPerThrust, 0.0f, 1.0f);
            currentPowerNormalized = ThrusterIncrementCurve.Evaluate(currentThrusterNormalizedForward);
        }
        else
        {
            currentThrusterNormalizedBackward = Mathf.Clamp(currentThrusterNormalizedBackward + diffPerThrust, 0.0f, 1.0f);
            currentPowerNormalized = ThrusterIncrementCurve.Evaluate(currentThrusterNormalizedBackward);
        }

        float power = currentPowerNormalized * maxPower;
        return power;
    }

    public void Reset(PhysicsBody cockpit)
    {
        Force = Vector3.zero;
        currentThrusterNormalizedForward = 0.0f;
        currentThrusterNormalizedBackward = 0.0f;
        cockpit.AddLinearForce("VerticalThruster", Vector3.zero);

        IsEnabled = false;
        IsEnabledPositive = false;
        IsEnabledNegative = false;
    }
}
