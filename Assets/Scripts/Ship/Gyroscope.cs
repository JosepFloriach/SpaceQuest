using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour, ShipComponent
{
    public Vector3 Force
    {
        get;
        protected set;
    }

    public bool IsEnabled { get; private set; }

    public bool IsEnabledPositive { get; private set; }
    public bool IsEnabledNegative { get; private set; }

    public void AddPositive(PhysicsBody cockpit, float maxSpeed, float diff)
    {
        IsEnabled = true;
        IsEnabledPositive = true;
        IsEnabledNegative = false;
        cockpit.AddAngularMomentum(maxSpeed);
        //transform.rotation = Quaternion.AngleAxis(maxSpeed * Time.deltaTime, cockpit.Transform.forward) * transform.rotation;
    }

    public void AddNegative(PhysicsBody cockpit, float maxSpeed, float diff)
    {
        IsEnabled = true;
        IsEnabledPositive = false;
        IsEnabledNegative = true;
        cockpit.AddAngularMomentum(-maxSpeed);
        //transform.rotation = Quaternion.AngleAxis(-maxSpeed * Time.deltaTime, cockpit.Transform.forward) * transform.rotation;
    }

    public void Reset(PhysicsBody cockpit)
    {
        IsEnabled = false;
        IsEnabledPositive = false;
        IsEnabledNegative = false;
        cockpit.RemoveAngularMomentum();
    }
}
