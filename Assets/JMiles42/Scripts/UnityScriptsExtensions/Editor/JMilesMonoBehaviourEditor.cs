using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour),true)]
public class JMilesMonoBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var changedObj = target as object;
        EditorGUILayout.BeginHorizontal("Box");
        GUIContent CopyContent = new GUIContent("Copy Data", "Copies the data.");
        GUIContent PasteContent = new GUIContent("Paste Data", "Pastes the data.");
        if (GUILayout.Button(CopyContent)) CopyPaste.Copy(changedObj);
        if (GUILayout.Button(PasteContent)) CopyPaste.Paste(ref changedObj);
        EditorGUILayout.EndHorizontal();
        DrawDefaultInspector();
    }
}