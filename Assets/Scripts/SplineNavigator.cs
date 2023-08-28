using UnityEngine;
using UnityEngine.Splines;

//[ExecuteInEditMode]
public class SplineNavigator : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] protected GameObject objectToAnimate;
    [Range(0,1)]
    [SerializeField] private float currentNormalizedPosition;
    [Range(0, 1)]
    [SerializeField] private float currentNormalizedTarget;
    [SerializeField] private bool navigating;
    [SerializeField] private float speed;
    [SerializeField] private bool faceForward;

    private bool travelingForward = true;

    protected Vector3 prevPosition;
    protected float currentAngle = 0.0f;

    private void Start()
    {
        if (objectToAnimate != null)
        {
            objectToAnimate.transform.position = splineContainer.EvaluatePosition(0.0f);
        }        
    }

    public void SetObjectToAnimate(GameObject objectToAnimate)
    {
        this.objectToAnimate = objectToAnimate;
    }

    public void SetSpline(SplineContainer splineContainer)
    {
        this.splineContainer = splineContainer;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Navigate()
    {
        navigating = true;
    }

    public void Pause()
    {
        navigating = false;
    }

    public void SetTarget(float normalizedSplinePosition)
    {
        currentNormalizedTarget = normalizedSplinePosition;
        travelingForward = currentNormalizedTarget > currentNormalizedPosition;
    }

    public void SetInstantPosition(float normalizedSplinePosition)
    {
        objectToAnimate.transform.position = splineContainer.EvaluatePosition(normalizedSplinePosition);
        currentNormalizedPosition = normalizedSplinePosition;
    }

    protected virtual void OnNavigationTick()
    {
    }

    private void Update()
    {
        if (objectToAnimate == null)
        {
            return;
        }

        if (navigating)
        {
            OnNavigationTick();
            if (travelingForward)
            {
                navigating = currentNormalizedPosition < currentNormalizedTarget;
                currentNormalizedPosition += (speed * Time.deltaTime);
            }
            else
            {
                navigating = currentNormalizedPosition > currentNormalizedTarget;
                currentNormalizedPosition -= (speed * Time.deltaTime);
            }
            if (faceForward)
            {
                FaceObjectTowardsMovement();
            }

            prevPosition = objectToAnimate.transform.position;
            objectToAnimate.transform.position = splineContainer.EvaluatePosition(currentNormalizedPosition);
        }
    }
    protected void FaceObjectTowardsMovement()
    {
        Vector3 moveDirection = (objectToAnimate.transform.position - prevPosition).normalized;
        objectToAnimate.transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
        currentAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90.0f;
    }
}
