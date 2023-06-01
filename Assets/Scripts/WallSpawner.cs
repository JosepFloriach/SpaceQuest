using PathCreation;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathCreator))]
public class WallSpawner : MonoBehaviour
{
    private PathCreator spawningPath;

    public GameObject prefab;
    public GameObject holder;
    public float spacing = 3;

    const float minSpacing = .1f;

    private void Awake()
    {
        spawningPath = GetComponent<PathCreator>();
    }

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        if (spawningPath != null && prefab != null && holder != null)
        {
            DestroyObjects();

            VertexPath path = spawningPath.path;

            spacing = Mathf.Max(minSpacing, spacing);
            float dst = 0;

            while (dst < path.length)
            {
                Vector3 point = path.GetPointAtDistance(dst);
                Quaternion rot = Quaternion.Euler(0.0f, 0.0f, Random.Range(0, 360.0f));
                Instantiate(prefab, point, rot, holder.transform);
                dst += spacing;
            }
        }
    }

    private void OnDestroy()
    {
        DestroyObjects();
    }

    private void DestroyObjects()
    {
        int numChildren = holder.transform.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            Destroy(holder.transform.GetChild(i).gameObject);
        }
    }

    private void OnValidate()
    {
        /*spawningPath = GetComponent<PathCreator>();
        if (spawningPath != null)
        {
            Generate();
        }*/
    } 
}
