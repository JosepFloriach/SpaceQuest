using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GravityFieldEventArgs
{
    public Player Player;
    public PhysicsBody Body;
    public PlanetGravity GravityField;
}

public class PlanetGravity : InteractableBase, Force
{ 
    [Header("Setup")]
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private float radius;
    [SerializeField] private float distance;
    //[SerializeField] private AnimationCurve divisionCurve;
    //[SerializeField] private AnimationCurve dumpingFactorCurve;

    [Header("Debug")]
    [SerializeField] private GameObject debugObject;
    [SerializeField] private int orbitDivisions = 10;

    public static event EventHandler<GravityFieldEventArgs> EnteredGravityField;
    public static event EventHandler<GravityFieldEventArgs> ExitedGravityField;

    
    //private float radius;
    private float prevSpeed;
    private Vector3 prevVelocity;

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

    public Vector3 Force
    {
        protected set;
        get;
    }

    /*protected void OnValidate()
    {
        GetComponent<CircleCollider2D>().radius = radius + distance;
    }*/

    protected override void OnStart()
    {
        //GetComponent<CircleCollider2D>().radius = (float)GetDistanceAtDivision(1.0f);
        GetComponent<CircleCollider2D>().radius = distance;
    }

    public void UpdateData(float radius, int orbitDivisions, float minOrbitSpeed, float maxOrbitSpeed, float maxDistance, float divisionCurveSlope, bool editCurves)
    {
#if UNITY_EDITOR
        /*this.orbitDivisions = orbitDivisions;
        this.radius = radius;

        if (!editCurves)
            return;

        Keyframe firstSpeed = new Keyframe(0.0f, 0.0f);
        Keyframe secondSpeed = new Keyframe(radius - 0.01f, 0.0f);
        Keyframe thirdSpeed = new Keyframe(radius, maxOrbitSpeed);
        Keyframe lastSpeed = new Keyframe(maxDistance, minOrbitSpeed);
        speedCurve = new AnimationCurve();
        speedCurve.AddKey(firstSpeed);
        speedCurve.AddKey(secondSpeed);
        speedCurve.AddKey(thirdSpeed);
        speedCurve.AddKey(lastSpeed);

        Keyframe firstDivision = new Keyframe(0, radius);
        Keyframe secondDivision = new Keyframe(1, maxDistance);
        divisionCurve = new AnimationCurve();

        firstDivision.outTangent = -divisionCurveSlope;
        secondDivision.inTangent = divisionCurveSlope;
        divisionCurve.AddKey(firstDivision);
        divisionCurve.AddKey(secondDivision);


        AnimationUtility.SetKeyRightTangentMode(divisionCurve, 0, AnimationUtility.TangentMode.Free);
        AnimationUtility.SetKeyLeftTangentMode(divisionCurve, 1, AnimationUtility.TangentMode.Free);
        AnimationUtility.SetKeyBroken(divisionCurve, 0, true);
        AnimationUtility.SetKeyBroken(divisionCurve, 1, true);*/
        
#endif
    }

    public override void StartInteraction(Player player, PhysicsBody body, Transform transform)
    {
        base.StartInteraction(player, body, transform);
        GravityFieldEventArgs args = new();
        args.Player = player;
        args.Body = body;
        args.GravityField = this;
        EnteredGravityField?.Invoke(typeof(PlanetGravity), args);
    }

    public override void ContinueInteraction(Player player, PhysicsBody body, Transform transform)
    {
        base.ContinueInteraction(player, body, transform);
        ComputeGravityForce(body, transform);
    }

    public override void EndInteraction(Player player, PhysicsBody body, Transform transform)
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
        prevSpeed = -1;
    }

    private float GetForceAtDistance(float distance)
    {
        return speedCurve.Evaluate(distance);
    }

    private void ComputeGravityForce(PhysicsBody body, Transform bodyTransform)
    {

        Vector3 directionToPlanetCenter = transform.position - bodyTransform.position;
        float forceAtraction = GetForceAtDistance(directionToPlanetCenter.magnitude);
        //float dotProduct = Vector2.Dot(body.LinearVelocity.normalized, directionToPlanetCenter.normalized);
        //float dumpingPercentage = dumpingFactorCurve.Evaluate(dotProduct);
        //forceAtraction -= (forceAtraction * dumpingPercentage);
        Vector3 force = directionToPlanetCenter.normalized * forceAtraction;

        // Trick to make easier the mechanic about getting speed from gravity acceleration.
        // Basically, the speed is never reduced in a gravity field, unless player is
        // throttling backwards.   
        if (body.LinearVelocity.magnitude < prevSpeed && !body.Transform.GetComponent<Cockpit>().ThrustingBackward)
        {
            body.LinearVelocity = body.LinearVelocity.normalized * prevSpeed;
        }

        prevSpeed = body.LinearVelocity.magnitude;
        body.AddLinearForce(ID, force);

        /*Vector3 directionToPlanetCenter = GetDirectionToPlanet(bodyTransform);
        float distanceToPlanetCenter = directionToPlanetCenter.magnitude;
        Vector3 optimalOrbitDirection = GetOptimalOrbitDirection(bodyTransform.position);
        bool facingOppositeDirection = Vector2.Dot(optimalOrbitDirection.normalized, body.LinearVelocity.normalized) < 0;
        if (facingOppositeDirection)
        {
            optimalOrbitDirection *= -1;
        }
        float a = (distanceToPlanetCenter * distanceToPlanetCenter - optimalOrbitDirection.magnitude * optimalOrbitDirection.magnitude + distanceToPlanetCenter * distanceToPlanetCenter) / (2 * distanceToPlanetCenter);
        float h = 0.0f;

        Vector3 verticalIntersection = Vector3.zero;
        Vector3 nextOptimalPosition = Vector3.zero;
        if (Mathf.Abs(a) > distanceToPlanetCenter)
        {
            a = -distanceToPlanetCenter * 2.0f;
            verticalIntersection = directionToPlanetCenter.normalized * a;            
        }
        else
        {
            h = Mathf.Sqrt(distanceToPlanetCenter * distanceToPlanetCenter - a * a);
            verticalIntersection = directionToPlanetCenter.normalized * (a - distanceToPlanetCenter);   
        }
        nextOptimalPosition = new Vector3(-verticalIntersection.y, verticalIntersection.x, 0.0f).normalized * h;
        if (facingOppositeDirection)
        {
            nextOptimalPosition *= -1;
        }
        
        Vector3 force = Vector3.zero;
        Vector3 optimalForce = (bodyTransform.position + verticalIntersection + nextOptimalPosition) - (bodyTransform.position + optimalOrbitDirection);
        if (Mathf.Abs(optimalOrbitDirection.magnitude - body.LinearVelocity.magnitude) < 0.1f)
        {
            if (Vector3.Angle(body.LinearVelocity, optimalOrbitDirection) < 1.0f)
            {
                body.LinearVelocity = optimalOrbitDirection;
            }
            else
            {
                body.LinearVelocity = body.LinearVelocity.normalized * optimalOrbitDirection.magnitude;
            }
            force = optimalForce;
        }
        else
        {
            float forceMagnitude = optimalForce.magnitude;
            force = (transform.position - bodyTransform.position).normalized * forceMagnitude;
        }
        // Trick to make easier the mechanic about getting speed from gravity acceleration.
        // Basically, the speed is never reduced in a gravity field, unless player is
        // throttling backwards.   
        if (body.LinearVelocity.magnitude < prevSpeed && !body.Transform.GetComponent<Cockpit>().ThrustingBackward)
        {
            body.LinearVelocity = body.LinearVelocity.normalized * prevSpeed;
        }
        prevSpeed = body.LinearVelocity.magnitude;

        body.AddLinearForce(ID, force);
        float angle = -Vector3.Angle(body.LinearVelocity, body.LinearVelocity + force);
        if (facingOppositeDirection)
        {
            angle *= -1;
        }
        body.AddAngularForce(ID, new Vector3(0.0f, 0.0f, angle));*/
    }

    private Vector3 GetDirectionToPlanet(Transform bodyTransform)
    {
        return bodyTransform.position - transform.position;
    }

    public Vector3 GetOptimalOrbitDirection(Vector3 bodyPosition)
    {
        Vector3 toCenterDir = transform.position - bodyPosition;
        Vector3 directionToPlanetCenter = bodyPosition - transform.position;
        float distanceToPlanetCenter = (directionToPlanetCenter).magnitude;
        return new Vector3(-toCenterDir.y, toCenterDir.x).normalized * GetOptimalOrbitSpeedAtDistance(distanceToPlanetCenter);
    }

    private float GetOptimalOrbitSpeedAtDistance(float distance)
    {
        return speedCurve.Evaluate(distance);
    }

    /*private float GetDistanceAtDivision(float value)
    {
        return divisionCurve.Evaluate(value);        
    }*/

    private void OnDrawGizmosSelected()
    {        
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
        /*float diffDivision = 1.0f / orbitDivisions;
        float currDiff = 0.0f;
        for (int divIdx = 0; divIdx <= orbitDivisions; ++divIdx)
        {
            float currDist = GetDistanceAtDivision(currDiff);
            float currSpeed = GetOptimalOrbitSpeedAtDistance(currDist);
#if UNITY_EDITOR
            Handles.Label(transform.position - new Vector3(currDist, 0.0f, 0.0f), currSpeed.ToString("0.00"));
#endif
            GizmosCustom.DrawCircle(transform.position, currDist, 30);
            currDiff += diffDivision;
        }

        GameObject go = null;
        go = FindObjectOfType<Cockpit>().gameObject;*/
        /*if (Selection.activeGameObject != null && Selection.activeGameObject.gameObject == debugObject)
        {
            go = Selection.activeGameObject.gameObject;
        }
        else if (Selection.activeGameObject != null && Selection.activeGameObject.gameObject == this.gameObject)
        {
            go = FindObjectOfType<Cockpit>().gameObject;
        }*/
        /*-if (go != null)
        {
            Vector3 directionToPlanetCenter = go.transform.position - transform.position;
            float distanceToPlanetCenter = (directionToPlanetCenter).magnitude;
            // Sign depends on ship direction
            Vector3 optimalOrbitDirection = GetOptimalOrbitDirection(go.transform.position);
            bool facingOpposite = false;
            if (Vector2.Dot(optimalOrbitDirection.normalized, go.transform.up) < 0)
            {
                facingOpposite = true;
                optimalOrbitDirection *= -1;
            }

            float a = (distanceToPlanetCenter * distanceToPlanetCenter - optimalOrbitDirection.magnitude * optimalOrbitDirection.magnitude + distanceToPlanetCenter * distanceToPlanetCenter) / (2 * distanceToPlanetCenter);
            float h = 0.0f;

            Vector3 prep = Vector3.zero;
            Vector3 p = Vector3.zero;
            float distanceRun = 0.0f;
            if (Mathf.Abs(a) > distanceToPlanetCenter)
            {
                a = -distanceToPlanetCenter * 2.0f;
                prep = directionToPlanetCenter.normalized * a;
                p = new Vector3(-prep.y, prep.x, 0.0f).normalized * h;
                distanceRun = (go.transform.position + prep + p - go.transform.position).magnitude;
            }
            else
            {
                h = Mathf.Sqrt(distanceToPlanetCenter * distanceToPlanetCenter - a * a);
                prep = directionToPlanetCenter.normalized * (a - distanceToPlanetCenter);
                // Sign depends on ship direction.
                p = new Vector3(-prep.y, prep.x, 0.0f).normalized * h;
                if (facingOpposite)
                {
                    p *= -1;
                }
                distanceRun = (go.transform.position + prep + p - go.transform.position).magnitude;
            }

            Vector3 force = (go.transform.position + prep + p) - (go.transform.position + optimalOrbitDirection);
            Gizmos.color = Color.green;
            GizmosCustom.DrawCircle(transform.position, distanceToPlanetCenter, 60);
            Gizmos.color = Color.blue;
            GizmosCustom.DrawCircle(go.transform.position, optimalOrbitDirection.magnitude, 60);
            //Gizmos.DrawLine(go.transform.position, go.transform.position + optimalOrbitDirection);
            Gizmos.DrawLine(go.transform.position, transform.position);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(go.transform.position, go.transform.position + prep);
            Gizmos.DrawLine(go.transform.position + prep, go.transform.position + prep + p);
            Gizmos.DrawLine(go.transform.position + optimalOrbitDirection, go.transform.position + prep + p);
            GizmosCustom.DrawCircle(go.transform.position + optimalOrbitDirection, force.magnitude, 20);
#if UNITY_EDITOR
            Handles.Label(go.transform.position + new Vector3(2.0f, 7.0f, 0.0f), "Optimal speed: " + optimalOrbitDirection.magnitude.ToString("0.00"));
            //Handles.Label(go.transform.position + new Vector3(2.0f, 5.0f, 0.0f), "Current speed: " + go.GetComponent<Cockpit>().PhysicsBody.LinearVelocity.magnitude.ToString("0.00"));
#endif
        }*/
    }
}
