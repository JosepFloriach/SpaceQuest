using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraGravity : InteractableBase, IForce
{
    [SerializeField] private float KillRadius;
    [SerializeField] private AnimationCurve SpeedCurve;
    [SerializeField] private float radius;
    [SerializeField] private float distance;

    public Vector3 Direction { get; protected set; }

    public GameObject GetObject()
    {
        return this.gameObject;
    }

    protected override void OnStart()
    {
        GetComponent<CircleCollider2D>().radius = 105;
    }

    public override void StartInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
        base.StartInteraction(player, cockpit, transform);

    }

    public override void ContinueInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
        base.ContinueInteraction(player, cockpit, transform);
        var directionToPlanet = GetDirectionToPlanet(transform);
        var distanceToPlanet = directionToPlanet.magnitude;
        if (distanceToPlanet <= KillRadius)
        {
            player.Kill();
        }
        else
        {
            Direction = directionToPlanet * GetForceAtRadius(distanceToPlanet);
            cockpit.AddLinearForce(ID, this);
        }
    }

    public override void EndInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
        base.EndInteraction(player, cockpit, transform);
    }

    private float GetForceAtRadius(float radius)
    {
        return SpeedCurve.Evaluate(radius);
    }

    private Vector3 GetDirectionToPlanet(Transform bodyTransform)
    {
        return transform.position - bodyTransform.position;
    }

    private void OnDrawGizmosSelected()
    {
        /*float diffDivision = (distance - radius) / orbitDivisions;
        float currDist = radius;
        for (int divIdx = 0; divIdx <= orbitDivisions; ++divIdx)
        {
            float currSpeed = GetOptimalOrbitSpeedAtDistance(currDist);
#if UNITY_EDITOR
            Handles.Label(transform.position - new Vector3(currDist, 0.0f, 0.0f), currSpeed.ToString("0.00"));
#endif
            GizmosCustom.DrawCircle(transform.position, currDist, 30);
            currDist += diffDivision;
        }*/
    }
}