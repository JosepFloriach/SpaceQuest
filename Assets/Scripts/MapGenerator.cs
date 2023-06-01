using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] float cubeSize = 1.0f;

    [Range(1, 20)]
    [SerializeField] int width = 1;
    [Range(1, 20)]
    [SerializeField] int height = 1;
    [Range(1, 20)]
    [SerializeField] int deep = 1;

    [SerializeField] Material cubeMaterial;

    private IModelFactory cubeFactory;

    // Start is called before the first frame update
    private void Start()
    {
        cubeFactory = new CubeFactory();
        Build();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Build();
        }
    }

    private void Build()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int h = 0; h < height; ++h)
        {
            for (int d = 0; d < deep; ++d)
            {
                for (int w = 0; w < width; ++w)
                {
                    GameObject child = new GameObject();
                    Vector3 position = new Vector3(w * cubeSize * 2, h * cubeSize * 2, d * cubeSize * 2);

                    child.name = "Cube" + h + d + w;
                    child.transform.SetParent(transform);
                    child.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
                    child.transform.SetPositionAndRotation(position, Quaternion.identity);

                    MeshFilter meshFilter = child.AddComponent<MeshFilter>();
                    MeshRenderer meshRenderer = child.AddComponent<MeshRenderer>();

                    meshFilter.sharedMesh = cubeFactory.Build();
                    meshRenderer.material = cubeMaterial;
                }
            }
        }
    }
}
