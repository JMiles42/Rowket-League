using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RegexStringAttribute))]
public class RegexStringDrawer: PropertyDrawer
{
	private const int helpHeight = 30;
	private const int textHeight = 16;
	private RegexStringAttribute regexStringAttribute
	{
		get { return (RegexStringAttribute)attribute; }
	}

	public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
	{
		if(IsValid(prop))
			return base.GetPropertyHeight(prop, label);

		return base.GetPropertyHeight(prop, label) + helpHeight;
	}

	public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
	{
		var textFeildPosition = position;
		textFeildPosition.height = textHeight;
		DrawTextFeild(textFeildPosition, prop, label);
		var helpPosition = EditorGUI.IndentedRect(position);
		helpPosition.y      += textHeight;
		helpPosition.height =  helpHeight;
		DrawHelpBox(helpPosition, prop);
	}

	private void DrawTextFeild(Rect position, SerializedProperty prop, GUIContent label)
	{
		EditorGUI.BeginChangeCheck();
		var value = EditorGUI.TextField(position, label, prop.stringValue);

		if(EditorGUI.EndChangeCheck())
			prop.stringValue = value;
	}

	private void DrawHelpBox(Rect position, SerializedProperty prop)
	{
		if(IsValid(prop))
			return;

		EditorGUI.HelpBox(position, regexStringAttribute.helpMessage, MessageType.Error);
	}

	private bool IsValid(SerializedProperty prop)
	{
		return Regex.IsMatch(prop.stringValue, regexStringAttribute.pattern);
	}
}