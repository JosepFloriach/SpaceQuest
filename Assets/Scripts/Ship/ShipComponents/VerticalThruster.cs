using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalThruster : MonoBehaviour, IForce1D
{
    [SerializeField] AnimationCurve ThrusterIncrementCurve;

    private float currentThrusterNormalizedForward = 0.0f;
    private float currentThrusterNormalizedBackward = 0.0f;

    public bool IsEnabled { get; private set; }
    public bool IsEnabledPositive { get; private set; }
    public bool IsEnabledNegative { get; private set; }

    private Vector3 PrevForce;

    public Vector3 Direction
    {
        protected set;
        get;
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }

    public void AddPositive(IPhysicsBody cockpit, float maxPower, float diffPerThrust)
    {
        IsEnabled = true;
        IsEnabledPositive = true;
        IsEnabledNegative = false;

        float currentPower = GetPower(maxPower, diffPerThrust, true);
        Direction = cockpit.Transform.up * currentPower;
        cockpit.AddLinearForce("VerticalThruster", this);
        currentThrusterNormalizedBackward = 0.0f;
        //Debug.Log(Direction);
    }

    public void AddNegative(IPhysicsBody cockpit, float maxPower, float diffPerThrust)
    {
        IsEnabled = true;
        IsEnabledPositive = false;
        IsEnabledNegative = true;

        float currentPower = GetPower(maxPower, diffPerThrust, false);
        Direction = (-cockpit.Transform.up * currentPower);
        cockpit.AddLinearForce("VerticalThruster", this);
        currentThrusterNormalizedForward = 0.0f;
        PrevForce = Direction;
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

    public void Reset(IPhysicsBody cockpit)
    {
        Direction = Vector3.zero;
        currentThrusterNormalizedForward = 0.0f;
        currentThrusterNormalizedBackward = 0.0f;
        cockpit.AddLinearForce("VerticalThruster", this);

        IsEnabled = false;
        IsEnabledPositive = false;
        IsEnabledNegative = false;
    }

}
