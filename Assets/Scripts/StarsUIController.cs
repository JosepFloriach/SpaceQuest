using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsUIController : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteHolder;
    [SerializeField] SpriteRenderer distorsionHolder;
    [SerializeField] List<Sprite> bonusImages;
    [SerializeField] AnimationCurve gravityDistorsionCurve;

    private Inventory inventory;
    private ShipSpawner shipSpawner;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        ReferenceValidator.NotNull(inventory, spriteHolder, distorsionHolder, bonusImages, gravityDistorsionCurve);
    }

    private void Start()
    {
        shipSpawner = FindObjectOfType<ShipSpawner>();
        ReferenceValidator.NotNull(shipSpawner);
    }

    private void Update()
    {
        int currentStars = inventory.GetItemCount("StarPickup");
        spriteHolder.sprite = bonusImages[currentStars];
        distorsionHolder.sprite = bonusImages[currentStars];
        if (shipSpawner.Ship != null)
        {
            Cockpit ship = shipSpawner.Ship;
            Vector2 distortion = Vector2.zero;
            IForce gravityForce = ship.PhysicsBody.GetLinearForce("PlanetGravity");
            if (gravityForce != null)
            {
                distortion = new Vector2(gravityDistorsionCurve.Evaluate(gravityForce.Direction.magnitude), 0.0f);                
            }   
            distorsionHolder.material.SetVector("_DistorsionPosition", distortion);
        }
    }
}
