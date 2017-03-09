using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Rigidbody))]
[CanEditMultipleObjects]
public class RigidbodyEditor : Editor
{
	public override void OnInspectorGUI ()
    {
		var body = target as Rigidbody;

        EditorGUILayout.BeginHorizontal("Box");
        body = EditorHelpers.CopyPastObjectButtons(body) as Rigidbody;
        EditorGUILayout.EndHorizontal();

        DrawDefaultInspector();
        EditorHelpers.Label("");
        EditorHelpers.Label("Changing the Velocity may cause issues.");
        EditorHelpers.Label("");
        body.velocity = EditorHelpers.DrawVector3("Velocity", body.velocity,Vector3.zero,false);
        body.angularVelocity = EditorHelpers.DrawVector3("Angular", body.angularVelocity,Vector3.zero,false);
    }
}
