using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Vector2))]
public class Vector2PropEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight * 1.25f;
        float centerAmount = (height - EditorGUIUtility.singleLineHeight)/2;
        float titleWidth = EditorHelpers.GetStringLengthinPix(label.text);
        float widthVector = position.width - (height * 3) - titleWidth;
        EditorGUI.LabelField(new Rect(position.x, position.y, titleWidth, height), label);
        EditorGUI.PropertyField(new Rect(position.x + titleWidth, position.y+ centerAmount, widthVector, height), property, GUIContent.none);

        var resetsContent = new GUIContent("R", "Resets the vector to  " + Vector2.zero);
        if (GUI.Button(new Rect(position.x + titleWidth + widthVector, position.y, height, height), resetsContent))
            property.vector2Value = Vector2.zero;

        var copyContent = new GUIContent("C", "Copies the vectors data.");
        if (GUI.Button(new Rect(position.x + titleWidth + widthVector + height, position.y, height, height), copyContent))
            CopyPaste.EditorCopy(property.vector2Value);

        var pasteContent = new GUIContent("P", "Pastes the vectors data.");
        if (GUI.Button(new Rect(position.x + titleWidth + widthVector + height + height, position.y, height, height), pasteContent))
            property.vector2Value = CopyPaste.Paste<Vector2>();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 1.25f;
    }
}
[CustomPropertyDrawer(typeof(Vector3))]
public class Vector3PropEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight * 1.25f;
        float centerAmount = (height - EditorGUIUtility.singleLineHeight)/2;
        float titleWidth = EditorHelpers.GetStringLengthinPix(label.text);
        float widthVector = position.width - (height * 3) - titleWidth;
        EditorGUI.LabelField(new Rect(position.x, position.y, titleWidth, height), label);
        EditorGUI.PropertyField(new Rect(position.x + titleWidth, position.y+ centerAmount, widthVector, height), property, GUIContent.none);

        var resetsContent = new GUIContent("R", "Resets the vector to  " + Vector3.zero);
        if (GUI.Button(new Rect(position.x + titleWidth + widthVector, position.y, height, height), resetsContent))
            property.vector3Value = Vector3.zero;

        var copyContent = new GUIContent("C", "Copies the vectors data.");
        if (GUI.Button(new Rect(position.x + titleWidth + widthVector + height, position.y, height, height), copyContent))
            CopyPaste.EditorCopy(property.vector3Value);

        var pasteContent = new GUIContent("P", "Pastes the vectors data.");
        if (GUI.Button(new Rect(position.x + titleWidth + widthVector + height + height, position.y, height, height), pasteContent))
            property.vector3Value = CopyPaste.Paste<Vector3>();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 1.25f;
    }
}
[CustomPropertyDrawer(typeof(Vector4))]
public class Vector4PropEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight * 1.25f;
        float centerAmount = (height - EditorGUIUtility.singleLineHeight)/2;
        float titleWidth = EditorHelpers.GetStringLengthinPix(label.text);
        float widthVector = position.width - (height * 3) - titleWidth;
        EditorGUI.LabelField(new Rect(position.x, position.y, titleWidth, height), label);
        EditorGUI.PropertyField(new Rect(position.x + titleWidth, position.y+ centerAmount, widthVector, height), property, GUIContent.none);

        var resetsContent = new GUIContent("R", "Resets the vector to  " + Vector4.zero);
        if (GUI.Button(new Rect(position.x + titleWidth + widthVector, position.y, height, height), resetsContent))
            property.vector4Value = Vector4.zero;

        var copyContent = new GUIContent("C", "Copies the vectors data.");
        if (GUI.Button(new Rect(position.x + titleWidth + widthVector + height, position.y, height, height), copyContent))
            CopyPaste.EditorCopy(property.vector4Value);

        var pasteContent = new GUIContent("P", "Pastes the vectors data.");
        if (GUI.Button(new Rect(position.x + titleWidth + widthVector + height + height, position.y, height, height), pasteContent))
            property.vector4Value = CopyPaste.Paste<Vector4>();
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 1.25f;
    }
}
