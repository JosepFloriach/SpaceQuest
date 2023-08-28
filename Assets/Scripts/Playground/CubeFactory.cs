using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFactory  : IModelFactory
{
    public Mesh Build()
    {
        Mesh mesh = new Mesh();
        List<Vector3> points = new List<Vector3>(){
            new Vector3(-1, 1,-1),
            new Vector3( 1, 1,-1),
            new Vector3(-1,-1,-1),
            new Vector3( 1,-1,-1),
            new Vector3(-1, 1, 1),
            new Vector3( 1, 1, 1),
            new Vector3(-1,-1, 1),
            new Vector3( 1,-1, 1),
        };

        List<int> triangles = new List<int>()
        {
            0, 3, 2,
            0, 1, 3,
            1, 5, 7,
            1, 7, 3,
            4, 0, 2,
            4, 2, 6,
            4, 6, 7,
            4, 7, 5,
            0, 4, 5,
            0, 5, 1,
            2, 3, 7,
            2, 7, 6,
        };

        List<Vector3> normals = new List<Vector3>()
        {

        };

        mesh.SetVertices(points);
        mesh.SetTriangles(triangles,0);
        mesh.RecalculateNormals();

        return mesh;
    }
}
