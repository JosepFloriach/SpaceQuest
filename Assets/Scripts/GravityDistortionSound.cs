using UnityEngine;

[RequireComponent(typeof(Cockpit))]
public class GravityDistortionSound : MonoBehaviour
{
    [SerializeField] private AnimationCurve volumeCurve;

    private SoundManager soundManager;
    private Cockpit ship;
    private AudioSource gravityDistortionSound;

    private bool wasPlaying;

    private void Awake()
    {
        ship = GetComponent<Cockpit>();
        soundManager = FindObjectOfType<SoundManager>();        
        gravityDistortionSound = soundManager.GetSound("GravityDistortion");
        ReferenceValidator.NotNull(soundManager, volumeCurve, gravityDistortionSound);
    }

    private void Update()
    {
        IForce gravityForce = ship.PhysicsBody.GetLinearForce("PlanetGravity");
        if (gravityForce != null)
        {
            if (!wasPlaying)
            {
                soundManager.PlaySound("GravityDistortion");
            }
            //gravityDistortionSound.volume = Mathf.Abs(((PlanetGravity)gravityForce).GetNormalizedDistanceToGravityCenter(transform.position) - 1);
            gravityDistortionSound.volume = volumeCurve.Evaluate(((PlanetGravity)gravityForce).GetNormalizedDistanceToGravityCenter(transform.position));
            wasPlaying = true;
        }
        else
        {
            soundManager.StopSound("GravityDistortion");
            wasPlaying = false;
        }
    }
}
