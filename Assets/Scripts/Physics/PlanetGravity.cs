using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GravityFieldEventArgs
{
    public Player Player;
    public IPhysicsBody Body;
    public PlanetGravity GravityField;
}

public class PlanetGravity : InteractableBase, IForce
{
    [Header("Setup")]
    [SerializeField] GravityFieldsParameters gravityFieldsParameters;
    
    [SerializeField] private float radius;
    [SerializeField] private float distance;
    [Header("Debug")]
    [SerializeField] private GameObject debugObject;
    [SerializeField] private int orbitDivisions = 10;

    public static event EventHandler<GravityFieldEventArgs> EnteredGravityField;
    public static event EventHandler<GravityFieldEventArgs> ExitedGravityField;

    private AnimationCurve speedCurve = null;
    private Dictionary<IPhysicsBody, float> orbitatingBodiesSpeeds = new();

    public void OnValidate()
    {
        speedCurve = null;
    }

    public float Radius
    {
        get
        {
            return radius;
        }
    }

    public float FieldLength
    {
        get
        {
            return distance - radius;
        }
    }

    public Vector3 Direction
    {
        protected set;
        get;
    }

    public float GetNormalizedDistanceToGravityCenter(Vector3 position)
    {
        float distanceToPlanetEdge = Vector3.Distance(position, transform.position);
        return (distanceToPlanetEdge - radius) / (distance - radius);
    }

    public GameObject GetObject()
    {
        return this.gameObject;
    }

    private void BuildSpeedCurve()
    {  
        speedCurve = new(
            new Keyframe(0.0f, 0.0f),
            new Keyframe(radius - 0.01f, 0.0f),
            new Keyframe(radius, gravityFieldsParameters.MaxForceByRadiusCurve.Evaluate(radius)),
            new Keyframe(distance, 0.001f));
    }

    protected override void OnStart()
    {
        GetComponent<CircleCollider2D>().radius = distance;
        ID = "PlanetGravity";
        BuildSpeedCurve();
    }

    public override void StartInteraction(Player player, IPhysicsBody body, Transform transform)
    {
        base.StartInteraction(player, body, transform);
        GravityFieldEventArgs args = new();
        args.Player = player;
        args.Body = body;
        args.GravityField = this;
        orbitatingBodiesSpeeds.Add(body, body.LinearSpeed);
        EnteredGravityField?.Invoke(typeof(PlanetGravity), args);
    }

    public override void ContinueInteraction(Player player, IPhysicsBody body, Transform transform)
    {
        base.ContinueInteraction(player, body, transform);
        ComputeGravityForce(body, transform);
    }

    public override void EndInteraction(Player player, IPhysicsBody body, Transform transform)
    {
        base.EndInteraction(player, body, transform);
        body.RemoveLinearForce(ID);
        GravityFieldEventArgs args = new();
        args.Player = player;
        args.Body = body;
        args.GravityField = this;
        body.ForceRotationToVelocity(false);
        body.ForceRotationToInverseVelocity(false);
        ExitedGravityField?.Invoke(typeof(PlanetGravity), args);
        orbitatingBodiesSpeeds.Remove(body);
    }

    public float GetForceAtDistance(float distance)
    {
        return speedCurve.Evaluate(distance);
    }

    private void ComputeGravityForce(IPhysicsBody body, Transform bodyTransform)
    {
        Vector3 directionToPlanetCenter = transform.position - bodyTransform.position;
        float forceAtraction = GetForceAtDistance(directionToPlanetCenter.magnitude);        
        Direction = directionToPlanetCenter.normalized * forceAtraction;

        // Trick to make easier the mechanic about getting speed from gravity acceleration.
        // Basically, the speed is never reduced in a gravity field, unless player is
        // throttling backwards.   
        Cockpit cockpit = body.Transform.GetComponent<Cockpit>();
        if (cockpit != null && !cockpit.ThrustingBackward &&
            body.LinearVelocity.magnitude < orbitatingBodiesSpeeds[body] &&             
            !body.IsFrozen)
        {
            body.LinearVelocity = body.LinearVelocity.normalized * orbitatingBodiesSpeeds[body];
        }

        orbitatingBodiesSpeeds[body] = body.LinearVelocity.magnitude;
        body.AddLinearForce(ID, this);
    }
 
    public Vector3 GetOptimalOrbitDirection(Vector3 bodyPosition)
    {
        Vector3 toCenterDir = transform.position - bodyPosition;
        Vector3 directionToPlanetCenter = bodyPosition - transform.position;
        float distanceToPlanetCenter = (directionToPlanetCenter).magnitude;
        return new Vector3(-toCenterDir.y, toCenterDir.x).normalized * GetForceAtDistance(distanceToPlanetCenter);
    }

    private float GetOptimalOrbitSpeedAtDistance(float distance)
    {
        return speedCurve.Evaluate(distance);
    }

    private void OnDrawGizmosSelected()
    {
        if (speedCurve == null)
        {
            BuildSpeedCurve();
        }

        float diffDivision = (distance - radius) / orbitDivisions;
        float currDist = radius;
        for (int divIdx = 0; divIdx <= orbitDivisions; ++divIdx)
        {
            float currSpeed = GetOptimalOrbitSpeedAtDistance(currDist);
#if UNITY_EDITOR
            Handles.Label(transform.position - new Vector3(currDist, 0.0f, 0.0f), currSpeed.ToString("0.00"));
#endif
            GizmosCustom.DrawCircle(transform.position, currDist, 30);
            currDist += diffDivision;
        }
    }
}
