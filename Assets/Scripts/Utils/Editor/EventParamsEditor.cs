using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Utils
{

    [CustomPropertyDrawer(typeof(EventListener))]
    public class EventParamsEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);

            var targetObjProperty = property.FindPropertyRelative("externalObject");
            var scriptIdxProperty = property.FindPropertyRelative("scriptIdx");
            var actionIdxProperty = property.FindPropertyRelative("actionIdx");
            var targetScriptProperty = property.FindPropertyRelative("externalScript");
            var actionNameProperty = property.FindPropertyRelative("actionName");

            if (targetObjProperty.objectReferenceValue == null)
            {
                EditorGUI.PropertyField(position, targetObjProperty);
            }
            else
            {
                List<MonoBehaviour> scripts = GetMonoScripts(targetObjProperty.objectReferenceValue as GameObject);
                if (scripts.Count > 0)
                {
                    string[] scriptNames = GetScriptNames(scripts);
                    scriptIdxProperty.intValue = EditorGUI.Popup(new Rect(position.x, position.y, position.width * 0.4f, position.height), scriptIdxProperty.intValue, scriptNames);
                    targetScriptProperty.objectReferenceValue = scripts[scriptIdxProperty.intValue] as MonoBehaviour;

                    string[] actions = GetActionNames(scripts[scriptIdxProperty.intValue]);
                    if (actions.Length == 0)
                    {
                        EditorGUI.LabelField(new Rect(position.x + position.width * 0.4f, position.y, position.width * 0.4f, position.height), "No actions");
                    }
                    else
                    {
                        actionIdxProperty.intValue = EditorGUI.Popup(new Rect(position.x + position.width * 0.4f, position.y, position.width * 0.4f, position.height), actionIdxProperty.intValue, actions);
                        actionNameProperty.stringValue = actions[actionIdxProperty.intValue];
                    }
                    if (EditorGUI.LinkButton(new Rect(position.x + position.width * 0.8f, position.y, position.width * 0.1f, position.height), "Reset"))
                    {
                        targetObjProperty.objectReferenceValue = null;
                        scriptIdxProperty.intValue = 0;
                    }
                }
                else
                {
                    EditorGUI.LabelField(new Rect(position.x, position.y, position.width * 0.5f, position.height), "No Monobehaviours attached");
                    if (EditorGUI.LinkButton(new Rect(position.x + position.width * 0.5f, position.y, position.width * 0.1f, position.height), "Reset"))
                    {
                        targetObjProperty.objectReferenceValue = null;
                        scriptIdxProperty.intValue = 0;
                    }
                }
            }
            EditorGUI.EndProperty();
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        private List<MonoBehaviour> GetMonoScripts(GameObject obj)
        {
            List<MonoBehaviour> optionsList = new();
            foreach (var mono in obj.GetComponents<MonoBehaviour>())
            {
                optionsList.Add(mono);
            }
            return optionsList;
        }

        private string[] GetScriptNames(List<MonoBehaviour> scripts)
        {
            List<string> names = new();
            foreach (var script in scripts)
            {
                names.Add(script.GetType().Name);
            }
            return names.ToArray();
        }

        private string[] GetActionNames(MonoBehaviour script)
        {
            List<string> eventsList = new();
            EventInfo[] eventInfos = script.GetType().GetEvents();
            foreach (var eventInfo in eventInfos)
            {
                eventsList.Add(eventInfo.Name);
            }
            return eventsList.ToArray();
        }
    }

}