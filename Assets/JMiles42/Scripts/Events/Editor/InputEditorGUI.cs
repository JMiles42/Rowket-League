using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InputAxis))]
public class InputEditorGUI : PropertyDrawer
{
    private readonly float singleLine = EditorGUIUtility.singleLineHeight;
    private readonly float Height = EditorGUIUtility.singleLineHeight * 2;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return Height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var Axis = new EditorEntry("Axis", property.FindPropertyRelative("Axis"));
        var m_Value = new EditorEntry("Value", property.FindPropertyRelative("m_Value"));
        var ValueInverted = new EditorEntry("Inverted Resualt", property.FindPropertyRelative("ValueInverted"));

        var halfRowWidth = position.width / 2;
        var thirdRowWidth = position.width / 3;

        EditorGUI.LabelField(new Rect(position.x + halfRowWidth - Axis, position.y, Axis, singleLine),
            Axis);
        EditorGUI.PropertyField(
            new Rect(position.x + halfRowWidth, position.y, halfRowWidth, singleLine), Axis,
            GUIContent.none);

        EditorGUI.LabelField(new Rect(position.x, position.y + singleLine, m_Value, singleLine),
            m_Value);
        EditorGUI.PropertyField(
            new Rect(position.x + m_Value, position.y + singleLine, thirdRowWidth,
                singleLine), m_Value, GUIContent.none);

        EditorGUI.LabelField(
            new Rect(position.x + thirdRowWidth * 2 - ValueInverted / 2, position.y + singleLine, ValueInverted,
                singleLine),
            ValueInverted);
        EditorGUI.PropertyField(
            new Rect(position.x + thirdRowWidth * 2 + ValueInverted / 2, position.y + singleLine, halfRowWidth / 2,
                singleLine), ValueInverted, GUIContent.none);

        property.serializedObject.ApplyModifiedProperties();
    }
}