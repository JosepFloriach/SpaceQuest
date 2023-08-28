#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PhysicsBodyBehaviour), editorForChildClasses: true)]
[CanEditMultipleObjects]
public class PhysicsBodyBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (Application.isPlaying && GUILayout.Button("Freeze"))
        {
            IPhysicsBody physicsBody = ((PhysicsBodyBehaviour)target).PhysicsBody;
            physicsBody.Freeze();
        }
        if (Application.isPlaying && GUILayout.Button("UnFreeze"))
        {
            IPhysicsBody physicsBody = ((PhysicsBodyBehaviour)target).PhysicsBody;
            physicsBody.UnFreeze();
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif