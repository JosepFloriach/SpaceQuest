#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

[CustomEditor(typeof(ForceApplier), editorForChildClasses: true)]
public class ForceApplierEditor : Editor
{
    private Transform transform;
    private SerializedProperty forceProperty;
    private Vector3 handlePosition;

    private bool editForce;
#if UNITY_EDITOR
    private Tool LastTool = Tool.None;
#endif

    private void OnEnable()
    {
#if UNITY_EDITOR
        LastTool = Tools.current;
        Tools.current = Tool.None;
#endif

        forceProperty = serializedObject.FindProperty("direction");
        transform = (serializedObject.targetObject as Component).transform;
        handlePosition = forceProperty.vector3Value;
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        Tools.current = LastTool;
#endif
    }

    protected virtual void OnSceneGUI()
    {
        serializedObject.Update();
        Handles.color = Color.red;
        handlePosition = Handles.PositionHandle(transform.position + handlePosition, Quaternion.identity) - transform.position;        
        Handles.DrawLine(transform.position, transform.position + handlePosition);
        forceProperty.vector3Value = handlePosition;
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("applyOnStart"));
        editForce = GUILayout.Toggle(editForce,"Hide default Gizmo");
#if UNITY_EDITOR
        if (editForce)
        {
            Tools.current = Tool.None;
        }
        else
        {
            Tools.current = Tool.Move;
        }
#endif
        if (Application.isPlaying && GUILayout.Button("Trigger Force"))
        {
            ForceApplier forceApplier = (ForceApplier)target;
            forceApplier.ApplyForce();
        }
        if (GUILayout.Button("Reset"))
        {
            handlePosition = Vector3.zero;            
        }
        forceProperty.vector3Value = handlePosition;
        serializedObject.ApplyModifiedProperties();
    }
}
#endif