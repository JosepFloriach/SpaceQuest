using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject velocityMarker;
    [SerializeField] private GameObject directionMarker;
    [SerializeField] private GameObject planetMarker;

    [SerializeField] private SpriteRenderer overlaySprite;
    [SerializeField] private PlanetGravity currentGravityField;


    [SerializeField] private float correctionDirectionMarker;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float minMaxRange;

    private Cockpit cockpit;
    private LineRenderer optimalDirectionLineRenderer;
    private bool isEnabled = false; 

    private void Awake()
    {
        cockpit = GetComponent<Cockpit>();
        optimalDirectionLineRenderer = velocityMarker.GetComponent<LineRenderer>();
    }

    private void Start()
    {
        PlanetGravity.EnteredGravityField += OnGravityFieldEntered;
        PlanetGravity.ExitedGravityField += OnGravityFieldExited;
        //UpdateDirectionMarkerPosition();
        Disable();
    }

    private void OnGravityFieldEntered(object sender, GravityFieldEventArgs args)
    {
        currentGravityField = args.GravityField;
        Enable();
    }

    private void OnGravityFieldExited(object sender, GravityFieldEventArgs args)
    {
        currentGravityField = null;
        Disable();
    }

    private void Enable()
    {
        overlay.SetActive(true);

        isEnabled = true;
        optimalDirectionLineRenderer.positionCount = 2;
        optimalDirectionLineRenderer.SetPosition(0, Vector3.zero);
        optimalDirectionLineRenderer.SetPosition(1, Vector3.zero);        
    }

    private void Disable()
    {
        isEnabled = false;
        optimalDirectionLineRenderer.positionCount = 0;
        overlay.SetActive(false);
    }

    private void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        UpdatePlanetMarkerPosition();
        UpdateVelocityMarkerPosition();
        UpdateDirectionMarkerPosition();
        UpdateOverlaySize();
    }

    private void UpdatePlanetMarkerPosition()
    {
        Vector3 directionToBodyMass = currentGravityField.transform.position - cockpit.transform.position;
        planetMarker.transform.position = transform.position + directionToBodyMass.normalized * GetOverlayRadius();
    }

    private void UpdateDirectionMarkerPosition()
    {
        Vector3 optimalOrbitDirection = currentGravityField.GetOptimalOrbitDirection(transform.position);
        directionMarker.transform.position = transform.position + optimalOrbitDirection.normalized * GetOverlayRadius();
    }


    private void UpdateOverlaySize()
    {
        if (cockpit == null || currentGravityField == null)
            return;

        float currentSize = currentGravityField.GetOptimalOrbitDirection(transform.position).magnitude * 2;
        overlaySprite.size = new Vector3(currentSize , currentSize);
        if (currentSize < 7.18f)
        {            
            directionMarker.SetActive(false);
            planetMarker.SetActive(false);
            optimalDirectionLineRenderer.enabled = false;
        }
        else
        {            
            directionMarker.SetActive(true);
            planetMarker.SetActive(true);
            optimalDirectionLineRenderer.enabled = true;
        }
    }

    private void UpdateVelocityMarkerPosition()
    {
        if (cockpit == null || currentGravityField == null)
            return;

        Vector3 optimalOribtDirection = currentGravityField.GetOptimalOrbitDirection(transform.position);
        float optimalSpeed = optimalOribtDirection.magnitude;
        float maxSpeed = optimalSpeed * 2.0f;
        if (maxSpeed == 0.0f)
        {
            return;
        }

        float maximalSpeedRadius = GetOverlayRadius() * 2;
        float currentSpeed = cockpit.PhysicsBody.LinearVelocity.magnitude;
        float normalizedSpeed = currentSpeed / maxSpeed;
        float normalizedRadius = normalizedSpeed;
        Vector3 velocityPositionInOverlay = transform.position + cockpit.PhysicsBody.LinearVelocity.normalized * (normalizedRadius * maximalSpeedRadius * overlaySprite.transform.localScale.y);

        optimalDirectionLineRenderer.startWidth = 0.3f;
        optimalDirectionLineRenderer.endWidth = 0.3f;
        optimalDirectionLineRenderer.SetPosition(0, transform.position + new Vector3(0.0f, 0.0f, 1.0f));
        optimalDirectionLineRenderer.SetPosition(1, velocityPositionInOverlay + new Vector3(0.0f, 0.0f, 1.0f));  
    }

    private float GetOverlayRadius()
    {
        return overlaySprite.localBounds.size.x / 2.0f - correctionDirectionMarker;
    }

    private void OnDrawGizmos()
    {
        cockpit = GetComponent<Cockpit>();
        if (currentGravityField == null)
            return;

        Gizmos.color = Color.red;
        Vector3 optimalDirection = currentGravityField.GetOptimalOrbitDirection(transform.position);
        float maximalSpeedRadius = GetOverlayRadius() * 2;
        Gizmos.DrawLine(transform.position, transform.position + optimalDirection.normalized * (maximalSpeedRadius * overlaySprite.transform.localScale.y));
    }
}
