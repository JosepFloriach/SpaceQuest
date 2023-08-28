using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [Serializable]
    private class ParallaxLayerSetup
    {
        public Transform transform;
        public float speedFactor;
    }

    [SerializeField] private List<ParallaxLayerSetup> infiniteLayers;
    [SerializeField] private List<ParallaxLayerSetup> singleElements;

    private Dictionary<Transform, ParallaxLayerSetup> singleElementsMap = new();
    private List<Vector2> spriteSizes = new();
    private Vector3 prevCameraPosition;
    private Transform cameraTransform;
    

    public void AddSingleElement(Transform transform, float speedFactor)
    {
        ParallaxLayerSetup setup = new();
        setup.transform = transform;
        setup.speedFactor = speedFactor;

        singleElementsMap.Add(transform, setup);
    }

    public void RemoveSingleElement(Transform transform)
    {
        singleElementsMap.Remove(transform);
    }

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        prevCameraPosition = cameraTransform.position;
        FillSpriteSizes();
        FillSingleElementsMap();
    }

    private void FillSingleElementsMap()
    {
        foreach(var element in singleElements)
        {
            ParallaxLayerSetup setup = new();
            setup.transform = element.transform;
            setup.speedFactor = element.speedFactor;

            singleElementsMap.Add(element.transform, setup);
        }
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - prevCameraPosition;
        UpdateSingleElements(deltaMovement);
        UpdateInfiniteLayers(deltaMovement);
        prevCameraPosition = cameraTransform.position;
    }

    private void UpdateInfiniteLayers(Vector3 deltaMovement)
    {
        for (int idx = 0; idx < infiniteLayers.Count; ++idx)
        {
            ParallaxLayerSetup layer = infiniteLayers[idx];
            layer.transform.position += deltaMovement * layer.speedFactor;

            if (Mathf.Abs(cameraTransform.position.x - layer.transform.position.x) >= spriteSizes[idx][0])
            {
                float offsetPositionX = (cameraTransform.position.x - layer.transform.position.x) % spriteSizes[idx][0];
                layer.transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, layer.transform.position.y);
            }
            if (Mathf.Abs(cameraTransform.position.y - layer.transform.position.y) >= spriteSizes[idx][1])
            {
                float offsetPositionY = (cameraTransform.position.y - layer.transform.position.y) % spriteSizes[idx][1];
                layer.transform.position = new Vector3(layer.transform.position.x, cameraTransform.position.y + offsetPositionY);
            }
        }
    }
    
    private void UpdateSingleElements(Vector3 deltaMovement)
    {
        foreach (var setup in singleElementsMap)
        {
            setup.Key.transform.position += setup.Value.speedFactor * deltaMovement;
        }
    }

    private void FillSpriteSizes()
    {
        foreach (ParallaxLayerSetup layer in infiniteLayers)
        {
            Sprite sprite = layer.transform.GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            spriteSizes.Add(new Vector2(texture.width / sprite.pixelsPerUnit, texture.height / sprite.pixelsPerUnit));
        }
    }
}
