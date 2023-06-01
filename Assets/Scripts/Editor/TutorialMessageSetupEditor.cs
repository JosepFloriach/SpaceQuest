using UnityEditor;
using UnityEngine;

/*[CustomPropertyDrawer(typeof(TutorialTypeSetup))]
public class TutorialMessageSetupDrawer : PropertyDrawer
{
    int _type;
    float timeValue;

    string[] _choices = new string[] { "By Time", "By Next Button", "By Custom Event" };

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(rect, label, property);
        SerializedProperty propertyEnum = property.FindPropertyRelative("howToTriggerNextMessage");

        EditorGUI.LabelField(rect, "How to Trigger Next Message: ");
        _type = EditorGUI.Popup(new Rect(rect.x + 200.0f, rect.y, rect.width - 200.0f,EditorGUIUtility.singleLineHeight), propertyEnum.intValue, _choices);

        var secondRect = new Rect(rect.x, rect.y + 20f, rect.width, EditorGUIUtility.singleLineHeight);

        switch (_type)
        {
            case 0:
                var time = property.FindPropertyRelative("time");                
                time.floatValue = EditorGUI.FloatField(secondRect, "Time in seconeds: ", time.floatValue);
                break;
            case 2:
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            propertyEnum.intValue = _type;
        }

        EditorGUI.EndProperty();
    }
}*/
