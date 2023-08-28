using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour
{
    [Header("Radar miniatures")]
    [SerializeField] private Sprite planet;
    [SerializeField] private Sprite blackHole;
    [SerializeField] private Sprite nebula;

    [Range(0,1)]
    [SerializeField] private float alpha;

    [SerializeField] float miniatureMinimumDistance;
    [SerializeField] float radarRadius;
    [SerializeField] float trackingRadius; 


    private class RadarMinature
    {
        public IInteractable type;
        public GameObject miniatureObject;
    }

    private Dictionary<GameObject, RadarMinature> trackedObjects = new();

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = trackingRadius;
    }

    private void Update()
    {
        foreach (var trackedObject in trackedObjects)
        {
            Vector3 radarPosition = GetPositionInRadar(trackedObject.Key.transform.position);            
            trackedObject.Value.miniatureObject.transform.position = radarPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {            
            Sprite miniatureSprite = GetMiniature(other.gameObject);
            if(miniatureSprite != null)
            {
                var miniatureObject = new GameObject();
                var spriteComp = miniatureObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                spriteComp.sprite = miniatureSprite;
                spriteComp.color = new Color(spriteComp.color.r, spriteComp.color.g, spriteComp.color.b, alpha);

                var radarMiniature = new RadarMinature();
                radarMiniature.type = interactable;
                radarMiniature.miniatureObject = miniatureObject;
                radarMiniature.miniatureObject.transform.parent = transform;
                trackedObjects.Add(other.gameObject, radarMiniature);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {        
        if (trackedObjects.ContainsKey(other.gameObject))
        {
            Destroy(trackedObjects[other.gameObject].miniatureObject);
            trackedObjects.Remove(other.gameObject);
        }
    }

    private Vector3 GetPositionInRadar(Vector3 worldPosition)
    {
        Vector3 directionToPosition = (worldPosition - transform.position).normalized;
        return transform.position + directionToPosition * radarRadius;
    }

    private Sprite GetMiniature(GameObject gameObj)
    {
        if (gameObj.tag == "Nebula")
        {
            return nebula;
        }
        else if (gameObj.tag == "BlackHole")
        {
            return blackHole;
        }
        else if (gameObj.tag == "Planet")
        {
            return planet;
        }
        
        return null;
    }
}
