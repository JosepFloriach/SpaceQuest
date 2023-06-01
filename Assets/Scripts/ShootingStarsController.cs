using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarsController : MonoBehaviour
{
    [SerializeField] private GameObject shootingStarPrefab;
    [SerializeField] private float probabilityLeft;
    [SerializeField] private float probabilityRight;
    [SerializeField] private float maxActiveStarsLeft;
    [SerializeField] private float maxActiveStartsRight;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;


    private int currentAliveStarsLeft = 0;
    private int currentAliveStarsRight = 0;

    private Dictionary<GameObject, int> activeStarsSide = new();

    private ParallaxEffect parallax;

    private void Awake()
    {
        parallax = FindObjectOfType<ParallaxEffect>();
    }

    private void Start()
    {
        ParticleSystemLifeCycle.Death += OnDeath;
    }

    // Update is called once per frame
    private void Update()
    {
        float value = Random.Range(0.0f, 1.0f);
        /*if (value <= probabilityLeft && currentAliveStarsLeft < maxActiveStarsLeft)
        {
            var shootingStarInstance = Instantiate(shootingStarPrefab);
            shootingStarInstance.transform.parent = transform;
            //shootingStarInstance.GetComponentInChildren<ParticleSystem>().startSpeed = Random.Range(minSpeed, maxSpeed);
            shootingStarInstance.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 1.0f));
            currentAliveStarsLeft++;
            activeStarsSide.Add(shootingStarInstance.gameObject, -1);
            parallax.AddSingleElement(shootingStarInstance.transform, 0.99f);
        }*/
        if (value >= probabilityRight && currentAliveStarsRight < maxActiveStartsRight)
        {
            var shootingStarInstance = Instantiate(shootingStarPrefab);
            shootingStarInstance.transform.parent = transform;
            //shootingStarInstance.GetComponentInChildren<ParticleSystem>().startSpeed = Random.Range(minSpeed, maxSpeed);
            shootingStarInstance.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 1.0f));
            currentAliveStarsRight++;
            activeStarsSide.Add(shootingStarInstance.gameObject, 1);
            //parallax.AddSingleElement(shootingStarInstance.transform, 0.99f);
        }
    }

    private void OnDeath(object sender, ParticleSystem particleSystem)
    {
        if (activeStarsSide.ContainsKey(particleSystem.gameObject))
        {
            if (activeStarsSide[particleSystem.gameObject] == -1)
            {
                currentAliveStarsLeft--;                
            }
            else
            {
                currentAliveStarsRight--;
            }
            parallax.RemoveSingleElement(particleSystem.transform);
        }
    }
}