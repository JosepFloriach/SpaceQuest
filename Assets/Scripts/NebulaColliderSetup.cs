using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaColliderSetup : MonoBehaviour
{
    [SerializeField] private GameObject nebulaBehaviourProtype;
    [SerializeField] private float colliderRadius;

    private ParticleSystem particlesSystem;
    private bool instantiated;
    private GameObject colliderInstance;

    private void Awake()
    {
        particlesSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        particlesSystem.trigger.AddCollider(FindObjectOfType<Cockpit>());
    }

    private void OnEnable()
    {
        NebulaBehaviour.NebulaExit += OnNebulaExit;
    }

    private void OnDisable()
    {
        NebulaBehaviour.NebulaExit -= OnNebulaExit;
    }

    public void OnParticleTrigger()
    {
        // Hack to overcome a limitation by Unity. Triggers module does not expose any information about the
        // colliding gameObject. To workaround this, normal 2D collider is instantiated in the same particle
        // location on ParticleTrigger enter. This way, the ship can react normally with the OnTrigger2D
        // provided by the instantiated collider.
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
        int enterCount = particlesSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int exitCount = particlesSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        if (!instantiated && enterCount > 1)
        {
            ParticleSystem.Particle p = enter[0];
            colliderInstance = Instantiate(nebulaBehaviourProtype, p.position, Quaternion.identity);
            colliderInstance.transform.parent = transform;
            colliderInstance.GetComponent<CircleCollider2D>().radius = colliderRadius;
            instantiated = true;
        }
    }

    public void OnNebulaExit(GameObject nebula)
    {
        if (nebula == gameObject)
        {
            Destroy(colliderInstance);
            instantiated = false;
        }
    }

    private void OnDrawGizmos()
    {
        GizmosCustom.DrawCircle(transform.position, colliderRadius, 12);
    }
}
