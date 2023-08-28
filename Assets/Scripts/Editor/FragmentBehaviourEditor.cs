#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

[CustomEditor(typeof(FragmentBehaviour))]
public class FragmentBehaviourEditor : Editor
{
    private SerializedProperty destructionPrefabProperty;
    private SerializedProperty behaviourTypeProperty;
    private SerializedProperty splineNavigatorProperty;

    private GameObject targetGo;

    private void OnEnable()
    {
        behaviourTypeProperty = serializedObject.FindProperty("behaviour");
        destructionPrefabProperty = serializedObject.FindProperty("explosionPrototype");
        splineNavigatorProperty = serializedObject.FindProperty("splineNavigator");
        targetGo = (target as FragmentBehaviour).gameObject;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(destructionPrefabProperty);
        EditorGUILayout.PropertyField(behaviourTypeProperty);
        if (behaviourTypeProperty.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(splineNavigatorProperty);
        }
        if (GUILayout.Button("Add require setup for this behaviour"))
        {
            if (behaviourTypeProperty.enumValueIndex == 1)
            {
                AddSplineComponents();
            }
            if (behaviourTypeProperty.enumValueIndex == 2)
            {
                AddPhysicsComponents();
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void AddSplineComponents()
    {
        Transform splinesParentTransform = targetGo.transform.parent.Find("Splines");
        if (splinesParentTransform == null)
        {
            var splinesParentGO = new GameObject();
            splinesParentGO.transform.SetParent(targetGo.transform.parent.transform);
            splinesParentTransform = splinesParentGO.transform;
            splinesParentTransform.name = "Splines";
            splinesParentTransform.position = targetGo.transform.parent.position;
        }
        
        var splineGO = new GameObject();
        splineGO.name = targetGo.name;
        splineGO.transform.SetParent(splinesParentTransform);
        splineGO.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        var splineContainer = splineGO.AddComponent<SplineContainer>();
        splineContainer.AddSpline();
        splineContainer.Splines[0].Add(new BezierKnot(targetGo.transform.localPosition));
        splineContainer.Splines[0].Add(new BezierKnot(targetGo.transform.localPosition + new Vector3(10.0f, 0.0f, 0.0f)));

        var splineNavigator = targetGo.AddComponent<SplineNavigator>();
        splineNavigator.SetSpline(splineContainer);
        splineNavigator.SetSpeed(1.0f);
        splineNavigatorProperty.objectReferenceValue = splineNavigator;
    }

    private void AddPhysicsComponents()
    {
        targetGo.AddComponent<PhysicsBodyBehaviour>();
        targetGo.AddComponent<ForceApplier>();
        var rigidBody = targetGo.AddComponent<Rigidbody2D>();
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        rigidBody.simulated = true;
    }
}
#endif