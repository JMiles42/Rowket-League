using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Rigidbody))]
public class RigidbodyEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		var body = target as Rigidbody;
	    DrawDefaultInspector();
        EditorGUILayout.BeginVertical("box");
        Label("Warning!!! Changing Velocity may not work.");

        DrawVector3("Velocity", body.velocity);
	    DrawVector3("Angular", body.angularVelocity);
        EditorGUILayout.EndVertical();
    }

    Vector3 DrawVector3(string label, Vector3 vec, bool useVecOne = false)
    {
        EditorGUILayout.BeginHorizontal();
        Label(label);
        vec = EditorGUILayout.Vector3Field("", vec);
        GUIContent resetContent = new GUIContent("R", useVecOne ? "Resets the vector to (1,1,1)" : "Resets the vector to (0,0,0)");
        if (GUILayout.Button(resetContent, GUILayout.Width(25))) vec = useVecOne ? Vector3.one : Vector3.zero;
        GUIContent copyContent = new GUIContent("C", "Copies the vectors data.");
        if (GUILayout.Button(copyContent, GUILayout.Width(25))) CopyPaste.Copy(vec);
        GUIContent pasteContent = new GUIContent("P", "Pastes the vectors data.");
        if (GUILayout.Button(pasteContent, GUILayout.Width(25))) vec = CopyPaste.Paste<Vector3>();
        EditorGUILayout.EndHorizontal();
        return vec;
    }
    void Label(string label)
    {
        EditorGUILayout.LabelField(label, GUILayout.Width(label.Length * 10 + 5));
    }
}
