using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AsteroidsGroupBehaviour : MonoBehaviour, IDestructible, ISpreadable, IInteractable, IReseteable
{
    [SerializeField] private GameObject explosionPrototype;
    [SerializeField] private List<FragmentBehaviour> fragments;
    
    public string ID => "";

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        ReferenceValidator.NotNull(soundManager, explosionPrototype, fragments);
    }

    public void Destroy()
    {
        var explosion = GameObject.Instantiate(explosionPrototype, transform.parent);
        explosion.transform.position = transform.position;
        explosion.GetComponent<ParticleSystem>().Play();
        soundManager.PlaySound("Explosion");
        Spread();
    }

    public void Spread()
    {
        foreach (var fragment in fragments)
        {
            fragment.OnSpread();
        }
    }

    public void EndInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
    }

    public void StartInteraction(Player player, IPhysicsBody body, Transform transform)
    {
    }

    public void ContinueInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }
}
