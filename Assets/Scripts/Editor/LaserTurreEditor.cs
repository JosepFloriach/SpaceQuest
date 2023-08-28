#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LaserTurret), editorForChildClasses: true)]
[CanEditMultipleObjects]
public class LaserTurreEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectsToDestroy"));
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("ExplosionsPrefabs"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("DisableOnPickup"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletPrefab"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("distanceThreshold"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("shootSound"));
        if (Application.isPlaying && GUILayout.Button("Shoot"))
        {
            LaserTurret laserTurret = (LaserTurret)target;
            laserTurret.Shoot();
        }       
        serializedObject.ApplyModifiedProperties();
    }
}
#endif