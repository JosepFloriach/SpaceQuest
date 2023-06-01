using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeometryUtils
{
    public static Mesh CreateCircleMesh(float radius, int segments)
    {
        Mesh circleMesh = new Mesh();
        
        var vertexPositions = new List<Vector3>();
        var normals = new List<Vector3>();
        var uvs = new List<Vector2>();
        float angleDiff = (2 * Mathf.PI) / segments;
        float currAngle = 0.0f;

        // Define vertices, normals and uvs
        vertexPositions.Add(Vector3.zero);
        normals.Add(Vector3.forward);
        uvs.Add(new Vector2(0.5f, 0.5f));
        while(currAngle < 2*Mathf.PI)
        {
            float x = Mathf.Cos(currAngle);
            float y = Mathf.Sin(currAngle);
            vertexPositions.Add(new Vector2(x, y) * radius);
            normals.Add(Vector3.forward);

            uvs.Add(new Vector2(
                x.Remap(-1f, 1f, 0f, 1f), 
                y.Remap(-1f, 1f, 0f, 1f)));

            currAngle += angleDiff;
        }

        // Define triangles
        var triangles = new List<int>();
        for (int idx = 1; idx < segments; ++idx)
        {
            triangles.Add(0);
            triangles.Add(idx);
            triangles.Add(idx+1);
        }
        // last triangle connecting last vertex with the first one.
        triangles.Add(0);
        triangles.Add(segments);
        triangles.Add(1);

        // Set all data to the mesh
        circleMesh.SetVertices(vertexPositions);
        circleMesh.SetTriangles(triangles, 0);
        circleMesh.SetUVs(0, uvs);
        circleMesh.SetNormals(normals);


        return circleMesh;
    }

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static Vector3 Max(Vector3 a, Vector3 b)
    {
        if (a.magnitude > b.magnitude)
            return a;
        return b;
    }

}
