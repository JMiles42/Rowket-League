using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//[CustomEditor(typeof(StringListScriptableObject))]
public class StringListScriptableObjectEditor: Editor
{
	private ReorderableList list;

	private void OnEnable()
	{
		list                    = new ReorderableList(serializedObject, serializedObject.FindProperty("Strings"), true, true, true, true);
		list.drawHeaderCallback = rect => { EditorGUI.LabelField(rect, "Strings"); };

		list.drawElementCallback = (rect, index, isActive, isFocused) =>
		{
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.height -= 1;
			EditorGUI.PropertyField(rect, element, GUIContent.none);
		};
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}