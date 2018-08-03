using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;
//THIS IS NOT MY CODE
//obtained from
//https://gist.github.com/t0chas/34afd1e4c9bc28649311
//Though I have made a few changes
//Currently only adding the Copy paste buttons at the top of OnInspectorGUI()

[CustomEditor(typeof(Object), true, isFallback = true)]
[CanEditMultipleObjects]
public class CustomEditorBase: Editor
{
	private Dictionary<string, ReorderableListProperty> reorderableLists;

	protected virtual void OnEnable()
	{
		reorderableLists = new Dictionary<string, ReorderableListProperty>(10);
	}

	~CustomEditorBase()
	{
		reorderableLists.Clear();
		reorderableLists = null;
	}

	public override void OnInspectorGUI()
	{
		var changedObj = target as object;
		EditorGUILayout.BeginHorizontal("Box");
		changedObj = EditorHelpers.CopyPastObjectButtons(changedObj);
		EditorGUILayout.EndHorizontal();
		var cachedGuiColor = GUI.color;
		serializedObject.Update();
		var property = serializedObject.GetIterator();
		var next     = property.NextVisible(true);

		if(next)
		{
			do
			{
				GUI.color = cachedGuiColor;
				HandleProperty(property);
			}
			while(property.NextVisible(false));
		}

		serializedObject.ApplyModifiedProperties();
	}

	protected void HandleProperty(SerializedProperty property)
	{
		//Debug.LogFormat("name: {0}, displayName: {1}, type: {2}, propertyType: {3}, path: {4}", property.name, property.displayName, property.type, property.propertyType, property.propertyPath);
		var isdefaultScriptProperty = property.name.Equals("m_Script") && property.type.Equals("PPtr<MonoScript>") && (property.propertyType == SerializedPropertyType.ObjectReference) && property.propertyPath.Equals("m_Script");
		var cachedGUIEnabled        = GUI.enabled;

		if(isdefaultScriptProperty)
			GUI.enabled = false;

		//var attr = this.GetPropertyAttributes(property);
		if(property.isArray && (property.propertyType != SerializedPropertyType.String))
			HandleArray(property);
		else
			EditorGUILayout.PropertyField(property, property.isExpanded);

		if(isdefaultScriptProperty)
			GUI.enabled = cachedGUIEnabled;
	}

	protected void HandleArray(SerializedProperty property)
	{
		var listData = GetReorderableList(property);
		listData.IsExpanded.target = property.isExpanded;

		if((!listData.IsExpanded.value && !listData.IsExpanded.isAnimating) || (!listData.IsExpanded.value && listData.IsExpanded.isAnimating))
		{
			EditorGUILayout.BeginHorizontal();
			property.isExpanded = EditorGUILayout.ToggleLeft(string.Format("{0}[]", property.displayName), property.isExpanded, EditorStyles.boldLabel);
			EditorGUILayout.LabelField(string.Format("size: {0}", property.arraySize));
			EditorGUILayout.EndHorizontal();
		}
		else
		{
			if(EditorGUILayout.BeginFadeGroup(listData.IsExpanded.faded))
				listData.List.DoLayoutList();

			EditorGUILayout.EndFadeGroup();
		}
	}

	protected object[] GetPropertyAttributes(SerializedProperty property)
	{
		return GetPropertyAttributes<PropertyAttribute>(property);
	}

	protected object[] GetPropertyAttributes<T>(SerializedProperty property) where T: Attribute
	{
		var bindingFlags = BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		if(property.serializedObject.targetObject == null)
			return null;

		var targetType = property.serializedObject.targetObject.GetType();
		var field      = targetType.GetField(property.name, bindingFlags);

		if(field != null)
			return field.GetCustomAttributes(typeof(T), true);

		return null;
	}

	private ReorderableListProperty GetReorderableList(SerializedProperty property)
	{
		ReorderableListProperty ret = null;

		if(reorderableLists.TryGetValue(property.name, out ret))
		{
			ret.Property = property;

			return ret;
		}

		ret = new ReorderableListProperty(property);
		reorderableLists.Add(property.name, ret);

		return ret;
	}

#region Inner-class ReorderableListProperty
	private class ReorderableListProperty
	{
		private SerializedProperty _property;
		public  AnimBool           IsExpanded { get; private set; }
		/// <summary>
		///     ref http://va.lent.in/unity-make-your-lists-functional-with-reorderablelist/
		/// </summary>
		public ReorderableList List { get; private set; }
		public SerializedProperty Property
		{
			get { return _property; }
			set
			{
				_property               = value;
				List.serializedProperty = _property;
			}
		}

		public ReorderableListProperty(SerializedProperty property)
		{
			IsExpanded       = new AnimBool(property.isExpanded);
			IsExpanded.speed = 1f;
			_property        = property;
			CreateList();
		}

		~ReorderableListProperty()
		{
			_property = null;
			List      = null;
		}

		private void CreateList()
		{
			bool dragable = true, header = true, add = true, remove = true;
			List                       =  new ReorderableList(Property.serializedObject, Property, dragable, header, add, remove);
			List.drawHeaderCallback    += rect => _property.isExpanded = EditorGUI.ToggleLeft(rect, _property.displayName, _property.isExpanded, EditorStyles.boldLabel);
			List.onCanRemoveCallback   += list => { return List.count > 0; };
			List.drawElementCallback   += drawElement;
			List.elementHeightCallback += idx => { return Mathf.Max(EditorGUIUtility.singleLineHeight, EditorGUI.GetPropertyHeight(_property.GetArrayElementAtIndex(idx), GUIContent.none, true)) + 4.0f; };
		}

		private void drawElement(Rect rect, int index, bool active, bool focused)
		{
			if(_property.GetArrayElementAtIndex(index).propertyType == SerializedPropertyType.Generic)
				EditorGUI.LabelField(rect, _property.GetArrayElementAtIndex(index).displayName);

			//rect.height = 16;
			rect.height =  EditorGUI.GetPropertyHeight(_property.GetArrayElementAtIndex(index), GUIContent.none, true);
			rect.y      += 1;
			EditorGUI.PropertyField(rect, _property.GetArrayElementAtIndex(index), GUIContent.none, true);
			List.elementHeight = rect.height + 4.0f;
		}
	}
#endregion

}