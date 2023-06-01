using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    void OnDrawGizmos()
    {
        int maxBounces = 50;
        Vector3 currOrigin = transform.position;
        Vector3 currDirection = transform.right;

        for (int idx = 0; idx < maxBounces; ++idx)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(currOrigin, currDirection, out hitInfo, 1000))
            {
                Gizmos.DrawLine(currOrigin, hitInfo.point);
                Gizmos.DrawSphere(hitInfo.point, 0.05f);
                Vector3 bounceDirection = Reflection(currDirection, hitInfo.normal).normalized;
                Gizmos.DrawLine(hitInfo.point, hitInfo.point + bounceDirection);

                currOrigin = hitInfo.point;
                currDirection = bounceDirection;
            }
            else
            {
                break;
            }
        }
    }

    Vector3 Reflection(Vector3 input, Vector3 normal)
    {
        return input - 2 * Vector3.Dot(input.normalized, normal) * normal;
    }

}
