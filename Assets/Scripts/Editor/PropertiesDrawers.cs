using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer(typeof(TeamManager.TeamInstance))]
public class TeamInstanceDrawer : PropertyDrawer
{
    private readonly float singleLine = EditorGUIUtility.singleLineHeight;
    private float Height = EditorGUIUtility.singleLineHeight * 4;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var name = new EditorEntry("Name", property.FindPropertyRelative("name"));
        var team = new EditorEntry("Value", property.FindPropertyRelative("team"));
        var mat = new EditorEntry("Material", property.FindPropertyRelative("mat"));

        var halfRowWidth = position.width / 2;

        EditorGUI.LabelField(new Rect(position.x + halfRowWidth - name, position.y, halfRowWidth / 2, singleLine),
            name);
        EditorGUI.PropertyField(
            new Rect(position.x + halfRowWidth, position.y, halfRowWidth, singleLine), name,
            GUIContent.none);

        EditorGUI.LabelField(new Rect(position.x, position.y + singleLine, mat, singleLine), mat);
        EditorGUI.PropertyField(
            new Rect(position.x + mat, position.y + singleLine, halfRowWidth - mat, singleLine), mat,
            GUIContent.none);

        EditorGUI.LabelField(new Rect(position.x + halfRowWidth, position.y + singleLine, halfRowWidth / 2, singleLine),
            team);
        EditorGUI.PropertyField(
            new Rect(position.x + halfRowWidth + team, position.y + singleLine, halfRowWidth - team,
                singleLine), team, GUIContent.none);

        property.serializedObject.ApplyModifiedProperties();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Height = singleLine * 2;
        return Height;
    }
}

[CustomPropertyDrawer(typeof(TeamManager.PlayerInstance))]
public class PlayerInstanceDrawer : PropertyDrawer
{
    private readonly float singleLine = EditorGUIUtility.singleLineHeight;
    private float Height = EditorGUIUtility.singleLineHeight * 2;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var player = new EditorEntry("Motor", property.FindPropertyRelative("_player"));
        var team = new EditorEntry("Team", property.FindPropertyRelative("team"));
        var Scores = new EditorEntry("Scores", property.FindPropertyRelative("Scores"));
        var BallHits = new EditorEntry("Ball Hits", property.FindPropertyRelative("BallHits"));

        float halfRowWidth = position.width / 2;

        EditorGUI.LabelField(new Rect(position.x, position.y + singleLine, player, singleLine),
            player);

        EditorGUI.PropertyField(
            new Rect((position.x + player), position.y + singleLine, halfRowWidth - player,
                singleLine),
            player, GUIContent.none);

        EditorGUI.LabelField(
            new Rect(position.x + halfRowWidth, position.y + singleLine, team, singleLine),
            team);
        EditorGUI.PropertyField(
            new Rect((position.x + halfRowWidth + team), position.y + singleLine,
                halfRowWidth - team, singleLine),
            team, GUIContent.none);

        EditorGUI.LabelField(new Rect(position.x, position.y + singleLine * 2, halfRowWidth / 2, singleLine), Scores);
        EditorGUI.PropertyField(
            new Rect(position.x + halfRowWidth / 2, position.y + singleLine * 2, halfRowWidth / 2, singleLine), Scores,
            GUIContent.none);

        EditorGUI.LabelField(
            new Rect(position.x + halfRowWidth, position.y + singleLine * 2, halfRowWidth / 2, singleLine),
            BallHits);
        EditorGUI.PropertyField(
            new Rect(position.x + halfRowWidth + halfRowWidth / 2, position.y + singleLine * 2, halfRowWidth / 2,
                singleLine), BallHits, GUIContent.none);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return Height = singleLine * 3;
    }
}

[CustomPropertyDrawer(typeof(PlayerDetails))]
public class PlayerDetailsEditor : PropertyDrawer
{
    private readonly float singleLine = EditorGUIUtility.singleLineHeight;
    private float Height = EditorGUIUtility.singleLineHeight * 2;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var Enabled = new EditorEntry("Enabled", property.FindPropertyRelative("Enabled"));
        var IsPlayer = new EditorEntry("Is Player", property.FindPropertyRelative("IsPlayer"));
        var Name = new EditorEntry(property.FindPropertyRelative("Name"));
        var aiMode = new EditorEntry("Mode", property.FindPropertyRelative("AiMoterMode"));
        var aiReaction = new EditorEntry("Reaction", property.FindPropertyRelative("aiReaction"));

        float halfRowWidth = position.width / 2;
        float thirdRowWidth = position.width / 3;

        float x = position.x;

        string playerOrAi = IsPlayer.Property.boolValue ? "Player" : "Ai";
        float playerOrAiWidth = EditorHelpers.GetStringLengthinPix(playerOrAi);

        EditorGUI.LabelField(new Rect(halfRowWidth - playerOrAiWidth, position.y, IsPlayer, singleLine),
            playerOrAi);

        EditorGUI.LabelField(
            new Rect(halfRowWidth, position.y, Enabled, singleLine), Enabled);
        EditorGUI.PropertyField(
            new Rect(halfRowWidth + Enabled, position.y, 10, singleLine), Enabled,
            GUIContent.none);
        if (IsPlayer.Property.boolValue)
            EditorGUI.LabelField(new Rect(halfRowWidth + 20 + Enabled, position.y, position.width, singleLine),
                Name);

        EditorGUI.LabelField(
            new Rect(x, position.y + singleLine, IsPlayer, singleLine), IsPlayer);
        x += IsPlayer;
        EditorGUI.PropertyField(
            new Rect(x, position.y + singleLine, thirdRowWidth - IsPlayer, singleLine), IsPlayer,
            GUIContent.none);
        x += thirdRowWidth - IsPlayer;

        EditorGUI.LabelField(
            new Rect(x, position.y + singleLine, aiMode, singleLine), aiMode);
        x += aiMode;
        EditorGUI.PropertyField(
            new Rect(x, position.y + singleLine, thirdRowWidth - aiMode, singleLine),
            aiMode, GUIContent.none);
        x += thirdRowWidth - aiMode;

        EditorGUI.LabelField(
            new Rect(x, position.y + singleLine, aiReaction, singleLine), aiReaction);
        x += aiReaction;
        EditorGUI.PropertyField(
            new Rect(x, position.y + singleLine, thirdRowWidth - aiReaction, singleLine),
            aiReaction, GUIContent.none);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Height = singleLine * 2;
        return Height;
    }
}

[CustomPropertyDrawer(typeof(PlayerFinalDetails))]
public class PlayerFinalDetailsEditor : PropertyDrawer
{
    private readonly float singleLine = EditorGUIUtility.singleLineHeight;
    private float Height = EditorGUIUtility.singleLineHeight * 2;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var detailsIndex = new EditorEntry("Index", property.FindPropertyRelative("detailsIndex"));
        var input = new EditorEntry("Input", property.FindPropertyRelative("input"));

        float halfRowWidth = position.width / 2;
        float thirdRowWidth = position.width / 3;

        EditorGUI.LabelField(
            new Rect(thirdRowWidth, position.y, detailsIndex, singleLine), detailsIndex);
        EditorGUI.PropertyField(
            new Rect(thirdRowWidth + detailsIndex, position.y, thirdRowWidth - detailsIndex, singleLine), detailsIndex,
            GUIContent.none);

        EditorGUI.LabelField(
            new Rect(thirdRowWidth + thirdRowWidth, position.y, input, singleLine), input);
        EditorGUI.PropertyField(
            new Rect(thirdRowWidth + thirdRowWidth + input, position.y, thirdRowWidth - input, singleLine), input,
            GUIContent.none);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return singleLine;
    }
}