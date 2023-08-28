using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour, IForce1D
{
    public Vector3 Direction
    {
        get;
        protected set;
    }

    public bool IsEnabled { get; private set; }

    public bool IsEnabledPositive { get; private set; }
    public bool IsEnabledNegative { get; private set; }

    public GameObject GetObject()
    {
        return this.gameObject;
    }

    public void AddPositive(IPhysicsBody cockpit, float maxSpeed, float diff)
    {
        IsEnabled = true;
        IsEnabledPositive = true;
        IsEnabledNegative = false;
        cockpit.AddAngularMomentum(maxSpeed);
    }

    public void AddNegative(IPhysicsBody cockpit, float maxSpeed, float diff)
    {
        IsEnabled = true;
        IsEnabledPositive = false;
        IsEnabledNegative = true;
        cockpit.AddAngularMomentum(-maxSpeed);
    }

    public void Reset(IPhysicsBody cockpit)
    {
        IsEnabled = false;
        IsEnabledPositive = false;
        IsEnabledNegative = false;
        cockpit.RemoveAngularMomentum();
    }
}
