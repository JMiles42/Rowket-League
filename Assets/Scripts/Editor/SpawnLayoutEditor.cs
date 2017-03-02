using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnLayout))]
public class SpawnLayoutEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var changedObj = target as SpawnLayout;
        EditorGUILayout.BeginHorizontal("Box");
        changedObj = EditorHelpers.CopyPastObjectButtons(changedObj) as SpawnLayout;
        EditorGUILayout.EndHorizontal();
        //DrawDefaultInspector();
        changedObj.Positions = EditorHelpers.Vector3ArrayDrawer(changedObj.Positions,Vector3.zero, true);
        changedObj.SpawnPointsAmounts = changedObj.Positions != null ? changedObj.Positions.Length : 0;
        EditorUtility.SetDirty(changedObj);
    }
}
