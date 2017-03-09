using UnityEngine;
using UnityEditor;
using System.Collections;

//[CanEditMultipleObjects]
[CustomEditor(typeof(Transform))]
public class TransformEditor : Editor
{
	static bool scaleToggle;
    static float scaleAmount = 1;


    public override void OnInspectorGUI()
    {
        var transform = target as Transform;
        serializedObject.Update();
        EditorGUILayout.BeginHorizontal("Box");
        transform = EditorHelpers.CopyPastObjectButtons(transform) as Transform;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        GUIContent ResetContent = new GUIContent("Reset Transform", "Reset Transforms in global space");
        GUIContent ResetLocalContent = new GUIContent("Reset Local Transform", "Reset Transforms in local space");
        if (GUILayout.Button(ResetContent)) transform.ResetPosRotScale();
        if (GUILayout.Button(ResetLocalContent)) transform.ResetLocalPosRotScale();
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginHorizontal();
        scaleToggle = EditorGUILayout.Toggle("Scale Presets", scaleToggle);
        if (scaleToggle) ScaleBtnsEnabled();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();


        EditorHelpers.Label(transform.parent == null?"Transform":"Local Transform");

        transform.localEulerAngles = EditorHelpers.DrawVector3("Rotation", transform.localEulerAngles, Vector3.zero);
        transform.localPosition = EditorHelpers.DrawVector3("Position", transform.localPosition, Vector3.zero);
        transform.localScale = EditorHelpers.DrawVector3("Scale   ", transform.localScale, Vector3.one);
        

        serializedObject.ApplyModifiedProperties();
    }
    void ScaleBtnsEnabled()
    {
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        ScaleArea();
        EditorGUILayout.BeginHorizontal();
        ScaleBtn(0.5f);
        ScaleBtn(1);
        ScaleBtn(2);
        ScaleBtn(5);
        ScaleBtn(10);
        ScaleBtn(20);
        ScaleBtn(50);
        ScaleBtn(100);
        EditorGUILayout.EndHorizontal ();
    }
    void ScaleBtn (float multi = 1)
	{
        GUIContent resetContent = new GUIContent(string.Format("{0}x", multi),string.Format("Resets the vector to ({0},{0},{0})", multi));

        if ( GUILayout.Button(resetContent))
		{
			var transform = target as Transform;
			transform.localScale = Vector3.one*multi;
		    scaleAmount = transform.localScale.x;
		}
	}

    void ScaleArea()
    {
		var transform = target as Transform;
        EditorGUILayout.BeginHorizontal ();
        GUIContent content = new GUIContent("Scale amount", "Set amount to uniformly scale the object");
        scaleAmount = EditorGUILayout.FloatField(content, scaleAmount);
        GUIContent scaleContent = new GUIContent("Set Scale", string.Format("Sets the scale ({0},{0},{0})",scaleAmount));
        if (GUILayout.Button(scaleContent)) transform.localScale = Vector3.one*scaleAmount;
        GUIContent scaleTimesContent = new GUIContent("Times Scale", string.Format("Sets the scale ({0},{1},{2})",transform.position.x*scaleAmount,transform.position.y*scaleAmount,transform.position.z*scaleAmount));
        if (GUILayout.Button(scaleTimesContent)) transform.localScale*=scaleAmount;
        EditorGUILayout.EndHorizontal();
    }
}
