using System.Collections;
using System.Collections.Generic;
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
        var name = property.FindPropertyRelative("name");
        var team = property.FindPropertyRelative("team");

        var halfRowWidth = position.width / 2;

        EditorGUI.LabelField(new Rect(position.x, position.y + singleLine, halfRowWidth / 2, singleLine), "Team Name");
        EditorGUI.PropertyField(
            new Rect(position.x + halfRowWidth / 2, position.y + singleLine, halfRowWidth / 2, singleLine), name,
            GUIContent.none);

        EditorGUI.LabelField(new Rect(position.x + halfRowWidth, position.y + singleLine, halfRowWidth / 2, singleLine),
            "Team Value");
        EditorGUI.PropertyField(
            new Rect(position.x + halfRowWidth + halfRowWidth / 2, position.y + singleLine, halfRowWidth / 2,
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
         var name = property.FindPropertyRelative("name");
         var player = property.FindPropertyRelative("_player");
         var team = property.FindPropertyRelative("team");
         var Scores = property.FindPropertyRelative("Scores");
         var BallHits = property.FindPropertyRelative("BallHits");

         var halfRowWidth = position.width / 2;

         const string nameStr = "Name";
         const string moterStr = "Moter";
         const string teamStr = "Team";

         float nameWidth = EditorHelpers.GetStringLengthinPix(nameStr);
         float moterWidth = EditorHelpers.GetStringLengthinPix(moterStr);
         float teamWidth = EditorHelpers.GetStringLengthinPix(teamStr);

         EditorGUI.LabelField(new Rect(position.x, position.y + singleLine, nameWidth, singleLine), nameStr);

         EditorGUI.PropertyField(
             new Rect(position.x + nameWidth, position.y + singleLine, halfRowWidth - nameWidth, singleLine), name,
             GUIContent.none);

         EditorGUI.LabelField(new Rect(position.x + halfRowWidth, position.y + singleLine, moterWidth, singleLine), moterStr);

         EditorGUI.PropertyField(
             new Rect((position.x + halfRowWidth + moterWidth), position.y + singleLine, halfRowWidth / 2 - moterWidth, singleLine),
             player, GUIContent.none);

         EditorGUI.LabelField(new Rect(position.x + halfRowWidth + halfRowWidth/2, position.y + singleLine, teamWidth, singleLine), teamStr);
         EditorGUI.PropertyField(
            new Rect((position.x + halfRowWidth + halfRowWidth / 2 + teamWidth), position.y + singleLine, halfRowWidth / 2 - teamWidth, singleLine),
            team, GUIContent.none);

         EditorGUI.LabelField(new Rect(position.x, position.y + singleLine * 2, halfRowWidth / 2, singleLine), "Scores");
         EditorGUI.PropertyField(
             new Rect(position.x + halfRowWidth / 2, position.y + singleLine * 2, halfRowWidth / 2, singleLine), Scores,
             GUIContent.none);

         EditorGUI.LabelField(
             new Rect(position.x + halfRowWidth, position.y + singleLine * 2, halfRowWidth / 2, singleLine),
             "Ball Hits");
         EditorGUI.PropertyField(
             new Rect(position.x + halfRowWidth + halfRowWidth / 2, position.y + singleLine * 2, halfRowWidth / 2,
                 singleLine), BallHits, GUIContent.none);
     }

     public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
     {
         Height = singleLine * 4;
         return Height;
     }
 }

[CustomPropertyDrawer(typeof(PlayerDetails))]
public class PlayerDetailsEditor:  PropertyDrawer
{
    private readonly float singleLine = EditorGUIUtility.singleLineHeight;
    private float Height = EditorGUIUtility.singleLineHeight * 2;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var IsPlayer = property.FindPropertyRelative("IsPlayer");
        var aiMode = property.FindPropertyRelative("aiMode");
        var aiReaction = property.FindPropertyRelative("aiReaction");

        float halfRowWidth = position.width / 2;
        float thirdRowWidth = position.width / 3;

        const string IsPlayerStr = "Is Player";
        const string aiModeStr = "Mode";
        const string aiReactionStr = "Reaction";

        float IsPlayerWidth = EditorHelpers.GetStringLengthinPix(IsPlayerStr);
        float aiModeStrWidth = EditorHelpers.GetStringLengthinPix(aiModeStr);
        float aiReactionStrWidth = EditorHelpers.GetStringLengthinPix(aiReactionStr);

        float x = position.x;


        EditorGUI.LabelField(new Rect(halfRowWidth, position.y, IsPlayerWidth, singleLine), IsPlayer.boolValue ? "Player" : "Ai");

        EditorGUI.LabelField(
            new Rect(x, position.y + singleLine, IsPlayerWidth, singleLine), IsPlayerStr);
        x += IsPlayerWidth;
        EditorGUI.PropertyField(
            new Rect(x, position.y + singleLine, thirdRowWidth - IsPlayerWidth, singleLine), IsPlayer,
            GUIContent.none);
        x += thirdRowWidth - IsPlayerWidth;

        EditorGUI.LabelField(
            new Rect(x, position.y + singleLine, aiModeStrWidth, singleLine), aiModeStr);
        x += aiModeStrWidth;
        EditorGUI.PropertyField(
            new Rect(x, position.y + singleLine, thirdRowWidth - aiModeStrWidth, singleLine),
            aiMode, GUIContent.none);
        x += thirdRowWidth - aiModeStrWidth;

        EditorGUI.LabelField(
            new Rect(x, position.y + singleLine, aiReactionStrWidth, singleLine), aiReactionStr);
        x += aiReactionStrWidth;
        EditorGUI.PropertyField(
            new Rect(x, position.y + singleLine, thirdRowWidth - aiReactionStrWidth, singleLine),
            aiReaction, GUIContent.none);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Height = singleLine * 2;
        return Height;
    }
}
