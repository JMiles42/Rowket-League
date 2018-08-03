using System;
using UnityEditor;
using UnityEngine;

public class EditorHelpers: Editor
{
	//[Obsolete("Use property drawer instead")]
	public static Vector3 DrawVector3(string label, Vector3 vec, Vector3 defualtValue, bool allowTransformDrop = false)
	{
		return DrawVector3(new GUIContent(label, "The vectors X,Y,Z values."), vec, defualtValue, allowTransformDrop);
	}

	//[Obsolete("Use property drawer instead")]
	public static Vector3 DrawVector3(GUIContent label, Vector3 vec, Vector3 defualtValue, bool allowTransformDrop = false)
	{
		EditorGUILayout.BeginHorizontal();
		vec = EditorGUILayout.Vector3Field(label, vec);

		if(allowTransformDrop)
		{
			var       transformContent = new GUIContent("", "Asign the vectors value from a transform Position");
			Transform transform        = null;
			transform = (Transform)EditorGUILayout.ObjectField(transformContent, transform, typeof(Transform), true, GUILayout.Width(50));

			if(transform != null)
			{
				EditorGUILayout.EndHorizontal();

				return transform.position;
			}
		}

		var resetContent = new GUIContent("R", "Resets the vector to  " + defualtValue);

		if(GUILayout.Button(resetContent, GUILayout.Width(25)))
			vec = defualtValue;

		var copyContent = new GUIContent("C", "Copies the vectors data.");

		if(GUILayout.Button(copyContent, GUILayout.Width(25)))
			CopyPaste.EditorCopy(vec);

		var pasteContent = new GUIContent("P", "Pastes the vectors data.");

		if(GUILayout.Button(pasteContent, GUILayout.Width(25)))
			vec = CopyPaste.Paste<Vector3>();

		EditorGUILayout.EndHorizontal();

		return vec;
	}

	public static void Label(string label)
	{
		EditorGUILayout.LabelField(label, GUILayout.Width(GetStringLengthinPix(label)));
	}

	public static object CopyPastObjectButtons(object obj)
	{
		var CopyContent  = new GUIContent("Copy Data",  "Copies the data.");
		var PasteContent = new GUIContent("Paste Data", "Pastes the data.");

		if(GUILayout.Button(CopyContent))
			CopyPaste.EditorCopy(obj);

		if(GUILayout.Button(PasteContent))
			CopyPaste.EditorPaste(ref obj);

		return obj;
	}

	[Obsolete("CUSTOM EDITOR BASE & Use property drawer instead")]
	public static Vector3[] Vector3ArrayDrawer(Vector3[] array, Vector3 defualtValue, bool allowTransformDrop = false)
	{
		var arrayLengthContent = new GUIContent("Array Length", "The length of the array");
		EditorGUILayout.BeginVertical("Box");
		var arrayLength = EditorGUILayout.IntField(arrayLengthContent, array == null? 0 : array.Length);

		if(arrayLength > 1000)
			arrayLength = 1000;

		if(arrayLength > 0)
		{
			if(array == null)
				array = new Vector3[arrayLength];
			else if(arrayLength > array.Length)
			{
				var list = new Vector3[arrayLength];
				//var temp = array;

				for(int i = 0, j = array.Length; i < j; i++)
					list[i] = array[i];

				array = list;
			}
			else if(arrayLength < array.Length)
			{
				var list = new Vector3[arrayLength];
				//var temp = array;

				for(int i = 0, j = arrayLength; i < j; i++)
					list[i] = array[i];

				array = list;
			}

			for(int i = 0, j = array.Length; i < j; i++)
			{
				EditorGUILayout.BeginHorizontal();
				Label("Index " + i);
				arrayLengthContent.text    = "";
				arrayLengthContent.tooltip = "Value at index: " + i;
				array[i]                   = DrawVector3(arrayLengthContent, array[i], defualtValue, allowTransformDrop);
				EditorGUILayout.EndHorizontal();
			}
		}
		else
			array = null;

		EditorGUILayout.EndVertical();

		return array;
	}

	public static float GetStringLengthinPix(string str)
	{
		return str.Length * 9;
	}
}

public class EditorString
{
	public readonly string String;
	public readonly float  Length;

	public EditorString(string str)
	{
		String = str;
		Length = EditorHelpers.GetStringLengthinPix(String);
	}

	public static implicit operator float(EditorString editorString)
	{
		return editorString.Length;
	}

	public static implicit operator string(EditorString editorString)
	{
		return editorString.String;
	}
}

public class EditorEntry
{
	public readonly string             String;
	public readonly float              Length;
	public          SerializedProperty Property;

	public EditorEntry(string str, SerializedProperty prop)
	{
		Property = prop;
		String   = str;
		Length   = EditorHelpers.GetStringLengthinPix(String);
	}

	public EditorEntry(SerializedProperty prop)
	{
		Property = prop;
		String   = prop.displayName;
		Length   = EditorHelpers.GetStringLengthinPix(String);
	}

	public static implicit operator SerializedProperty(EditorEntry editorString)
	{
		return editorString.Property;
	}

	public static implicit operator float(EditorEntry editorString)
	{
		return editorString.Length;
	}

	public static implicit operator string(EditorEntry editorString)
	{
		return editorString.String;
	}
}