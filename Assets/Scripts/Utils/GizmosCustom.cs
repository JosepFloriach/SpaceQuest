using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosCustom
{
   public static void DrawCircle(Vector3 position, float radius, int segments)
    {
        float diffAngle = (2 * Mathf.PI)/ segments;
        float currAngle = 0.0f;

        var points = new List<Vector2>();
        for (int segment = 0; segment < segments; ++segment)
        {
            float x = (radius * Mathf.Cos(currAngle)) + position.x;
            float y = (radius * Mathf.Sin(currAngle)) + position.y;
            points.Add(new Vector2(x, y));
            currAngle += diffAngle;
        }

        for (int idx = 0; idx < points.Count - 1; ++idx)
        {
            Gizmos.DrawLine(points[idx], points[idx + 1]);
        }
        Gizmos.DrawLine(points[points.Count - 1], points[0]);
    }
}
