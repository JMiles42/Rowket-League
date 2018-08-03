using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

//[CustomEditor(typeof(SpawnLayout))]
public class SpawnLayoutEditor: Editor
{
	private ReorderableList list;

	private void OnEnable()
	{
		list                    = new ReorderableList(serializedObject, serializedObject.FindProperty("Positions"), true, true, true, true);
		list.drawHeaderCallback = rect => { EditorGUI.LabelField(rect, "Spawn Locations"); };

		list.drawElementCallback = (rect, index, isActive, isFocused) =>
		{
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			EditorGUI.PropertyField(rect, element, GUIContent.none);
		};
	}

	public override void OnInspectorGUI()
	{
		var changedObj = target as SpawnLayout;
		EditorGUILayout.BeginHorizontal("Box");
		changedObj = EditorHelpers.CopyPastObjectButtons(changedObj) as SpawnLayout;
		EditorGUILayout.EndHorizontal();
		//DrawDefaultInspector();

		//serializedObject.Update();
		list.DoLayoutList();
		EditorUtility.SetDirty(changedObj);
	}
}