using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float bordersGap;


    private Player player;
    private ShipSpawner shipSpawner;
    private Cockpit cockpit;
    private new Camera camera;

    private float maxSpeed = 0.0f;
    //private float minSpeed = 0.0f;
    private PlanetGravity gravityField;

    private bool enableFollowing = false;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        shipSpawner = FindObjectOfType<ShipSpawner>();
        camera = GetComponent<Camera>();
        ReferenceValidator.NotNull(player, shipSpawner, camera);
    }

    private void OnEnable()
    {
        player.PlayerKilled += OnPlayerKilled;
        shipSpawner.ShipSpawned += OnShipSpawned;
        PlanetGravity.EnteredGravityField += OnGravityFieldEntered;
    }
    private void OnDisable()
    {
        player.PlayerKilled -= OnPlayerKilled;
        shipSpawner.ShipSpawned -= OnShipSpawned;
        PlanetGravity.EnteredGravityField -= OnGravityFieldEntered;
    }

    private void Update()
    {
        if (cockpit == null)
        {
            cockpit = FindObjectOfType<Cockpit>();
            if (cockpit != null)
            {
                PositionConstraint positionConstraint = GetComponent<PositionConstraint>();
                var constraintSource = new ConstraintSource();
                constraintSource.sourceTransform = cockpit.transform;
                constraintSource.weight = 1.0f;
                positionConstraint.AddSource(constraintSource);
                positionConstraint.constraintActive = true;
            }
        }

        /*transform.position = cockpit.transform.position;
        if (gravityField != null)
        {
            camera.orthographicSize = Mathf.Clamp(gravityField.GetOptimalOrbitDirection(cockpit.transform.position).magnitude, minDistance, maxDistance) + bordersGap;
        }
        else
        {
            camera.orthographicSize = Mathf.Clamp(cockpit.IPhysicsBody.LinearVelocity.magnitude, minDistance, maxDistance);
        }*/
    }

    private void OnShipSpawned()
    {
        enableFollowing = true;
    }

    private void OnPlayerKilled()
    {
        enableFollowing = false;
    }

    private void OnGravityFieldEntered(object sender, GravityFieldEventArgs args)
    {
        gravityField = args.GravityField;

    }
    private void OnGravityFieldExited(object sender, GravityFieldEventArgs args)
    {
        gravityField = null;
    }
}
