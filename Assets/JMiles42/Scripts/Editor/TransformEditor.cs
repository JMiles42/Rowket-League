using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Transform))]
public class TransformEditor : Editor
{
	bool scaleToggle;
    private float scaleAmount = 1;
	public override void OnInspectorGUI ()
	{
		var transform = target as Transform;
        EditorGUILayout.BeginVertical ("Box");
        EditorGUILayout.BeginHorizontal();
        GUIContent CopyContent = new GUIContent("Copy Data", "Copies the Transform data.");
        GUIContent PasteContent = new GUIContent("Paste Data", "Pastes the Transform data.");
        if (GUILayout.Button(CopyContent)) CopyPaste.Copy(new TransformData(transform.localEulerAngles, transform.localPosition, transform.localScale));
        if (GUILayout.Button(PasteContent))
        {
            TransformData transformData = CopyPaste.Paste<TransformData>();
            transform.localEulerAngles = transformData.Rotation;
            transform.localPosition = transformData.Position;
            transform.localScale = transformData.Scale;
        }
        EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical ();

        EditorGUILayout.BeginVertical ("Box");
        EditorGUILayout.BeginVertical ();
        EditorGUILayout.BeginHorizontal ();
        GUIContent ResetContent = new GUIContent("Reset Transform", "Reset Transforms in global space");
        GUIContent ResetLocalContent = new GUIContent("Reset Local Transform", "Reset Transforms in local space");
        if ( GUILayout.Button (ResetContent) ) transform.ResetPosRotScale();
		if ( GUILayout.Button (ResetLocalContent) ) transform.ResetLocalPosRotScale();
		EditorGUILayout.EndVertical ();
        EditorGUILayout.BeginHorizontal();
		scaleToggle = EditorGUILayout.Toggle ("Scale Presets", scaleToggle);
        if ( scaleToggle ) ScaleBtnsEnabled ();
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.EndHorizontal ();
        EditorGUILayout.EndVertical ();
		EditorGUILayout.LabelField ("Local Transform");
        
		transform.localEulerAngles = DrawVector3 ("Rotation", transform.localEulerAngles);
		transform.localPosition = DrawVector3    ("Position", transform.localPosition);
		transform.localScale = DrawVector3       ("Scale   ", transform.localScale,true);
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
    Vector3 DrawVector3(string label, Vector3 vec, bool useVecOne =false)
    {
        EditorGUILayout.BeginHorizontal();
        Label(label);
        vec = EditorGUILayout.Vector3Field("",vec);
        GUIContent resetContent = new GUIContent("R", useVecOne?"Resets the vector to (1,1,1)":"Resets the vector to (0,0,0)");
        if (GUILayout.Button(resetContent, GUILayout.Width(25))) vec = useVecOne?Vector3.one:Vector3.zero;
        GUIContent copyContent = new GUIContent("C", "Copies the vectors data.");
        if (GUILayout.Button(copyContent, GUILayout.Width(25))) CopyPaste.Copy(vec);
        GUIContent pasteContent = new GUIContent("P", "Pastes the vectors data.");
        if (GUILayout.Button(pasteContent, GUILayout.Width(25))) vec = CopyPaste.Paste<Vector3>();
        EditorGUILayout.EndHorizontal();
        return vec;
    }

    void Label(string label)
    {
        EditorGUILayout.LabelField(label, GUILayout.Width(label.Length*10+5));
    }

    void ScaleArea()
    {
		var transform = target as Transform;
        EditorGUILayout.BeginHorizontal ();
        GUIContent content = new GUIContent("Scale amount", "Set amount to uniformly scale the object");
        scaleAmount = EditorGUILayout.FloatField(content, scaleAmount);
        GUIContent scaleContent = new GUIContent("Set Scale", string.Format("Sets the scale ({0},{0},{0})",scaleAmount));
        if (GUILayout.Button(scaleContent)) transform.localScale = Vector3.one*scaleAmount;
        EditorGUILayout.EndHorizontal();
    }
}

public struct TransformData
{
    public Vector3 Rotation;
    public Vector3 Position;
    public Vector3 Scale;

    public TransformData(Vector3 _Rotation, Vector3 _Position, Vector3 _Scale)
    {
        Rotation = _Rotation;
        Position = _Position;
        Scale = _Scale;
    }
}
