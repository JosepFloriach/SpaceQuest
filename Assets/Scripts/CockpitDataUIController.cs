using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CockpitDataUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speed;

    private Player player;
    private ShipSpawner shipSpawner;
    private Cockpit cockpit;
    
    private void Awake()
    {
        cockpit = FindObjectOfType<Cockpit>();
        player = FindObjectOfType<Player>();
        shipSpawner = FindObjectOfType<ShipSpawner>();
    }

    private void Start()
    {
        player.PlayerKilled += DisableSpeed;
        shipSpawner.ShipSpawned += EnableSpeed;
    }

    private void Update()
    {
        speed.text = cockpit.PhysicsBody.LinearVelocity.magnitude.ToString("0");
    }

    private void DisableSpeed()
    {
        this.gameObject.SetActive(false);
    }

    private void EnableSpeed()
    {
        this.gameObject.SetActive(true);
    }
}
