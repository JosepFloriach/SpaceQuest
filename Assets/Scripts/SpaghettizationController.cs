//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Animations;
//using UnityEngine;
//using UnityEngine.U2D;
//using UnityEngine.U2D.Animation;

//public class SpaghettizationController : MonoBehaviour
//{
//    [SerializeField] private AnimationCurve spaghettizationCurve;
//    [SerializeField] private PlanetGravity currentGravityField;
//    [SerializeField] private Animator animator;
//    [Range(0,1)]
//    [SerializeField] private float animationTime;

//    [SerializeField] private Transform target;
//    [SerializeField] private Transform effectorTransform;
//    [SerializeField] private Transform rootTransform;
////    [SerializeField] List<Transform> boneTransforms;

//    private int iterationsPerFrame = 10;
//    private SpriteBone[] bones;

//    private void Start()
//    {
//        PlanetGravity.EnteredGravityField += OnPlanetGravityEnter;
//        PlanetGravity.ExitedGravityField += OnPlanetGravityExit;

//        InitFABRIK();
//    }

//    private void InitFABRIK()
//    {
//        bones = SpriteDataAccessExtensions.GetBones(GetComponent<SpriteRenderer>().sprite);
//    }

//    private void Update()
//    {
//        // Target must be always reachable in this context.
//        SolveIK(target.localPosition);


//        /*if (currentGravityField != null)
//        {           
//            Vector3 directionToBlackHole = (transform.position - currentGravityField.transform.position).normalized;
//            float distance = Vector3.Distance(transform.position, currentGravityField.transform.position + (directionToBlackHole * currentGravityField.Radius));
//            float normalizedDistance = distance / currentGravityField.FieldLength;
//            float spaghettizationForce = spaghettizationCurve.Evaluate(normalizedDistance);
//            Vector3 spaghettizationDirection = directionToBlackHole * spaghettizationForce;
//            animator.SetFloat("SpaghettizationNormalized", Mathf.Abs(normalizedDistance -1));
//            //Vector3 spaghettizationDirection = new Vector3(0.0f, spaghettizationForce, 0.0f);

//            //GetComponent<SpriteRenderer>().material.SetVector("_Displacement", spaghettizationDirection);
//        }*/
//    }

//    private void OnPlanetGravityEnter(object sender, GravityFieldEventArgs args)
//    {
//        if (args.GravityField.gameObject.tag == "BlackHole")
//        {
//            currentGravityField = args.GravityField;
//        }
//    }
//    private void OnPlanetGravityExit(object sender , GravityFieldEventArgs args)
//    {
//        animator.SetFloat("SpaghettizationNormalized", 0);
//        currentGravityField = null;
//    }

//    private void SolveIK(Vector3 target)
//    {
//        for (int iteration = 0; iteration < iterationsPerFrame; ++iteration)
//        {
//            DoBackward(target);
//            //DoForward(rootTransform);
//        }
//    }

//    private void DoBackward(Vector3 target)
//    {
//        Vector3 originalPosition = effectorTransform.localPosition;
//        effectorTransform.position = target;
//        /*Transform currentTransform = effectorTransform.parent;
//        Transform prevTransform = effectorTransform;
//        while(currentTransform.parent.GetComponent<SpriteRenderer>() == null)
//        {
//            Vector3 direction = (prevTransform.localPosition - currentTransform.localPosition).normalized;
//            Vector3 newPosition = currentTransform.localPosition + direction * (prevTransform.localPosition - currentTransform.localPosition).magnitude;
//            //bones[boneIdx].position = newPosition;
//            //boneTransforms[boneIdx].position = bones[boneIdx].position;
//            currentTransform.localPosition = newPosition;            
//            prevTransform = currentTransform;
//            currentTransform = prevTransform.parent;
//        }*/
//    }

//    private void DoForward(Vector3 startPosition)
//    {

//    }

//    private float GetLenghtBone(int boneIdx)
//    {
//        return bones[boneIdx].length;
//    }

//}
