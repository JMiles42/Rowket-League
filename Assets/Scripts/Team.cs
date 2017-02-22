using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Team", menuName = "Rowket/Team", order = 0)]
public class Team : JMilesScriptableObject
{
    public TeamType myTeam;
    public int score;
    public Color myTeamColour;

    public void Enable()
    {
        StopListening();
        StartListening();
    }

    public void Disable()
    {
#if !UNITY_EDITOR
        StopListening();
#endif
    }

    public void StartListening()
    {
        GetTeamsGoal().onGoal += TeamScored;
        GetTeamsGoal().onGoal += TeamManager.Instance.GoalScored;
    }
    public void StopListening()
    {
        GetTeamsGoal().onGoal -= TeamScored;
        GetTeamsGoal().onGoal -= TeamManager.Instance.GoalScored;
    }
    public void TeamScored()
    {
        score++;
    }

    public Goal GetTeamsGoal()
    {
        var goals = FindObjectsOfType<Goal>();
        foreach (var goal in goals)
            if (goal.myTeam == myTeam)
                return goal;
        return null;
    }
}

[Serializable]
public enum TeamType
{
    Red,
    Blue
}
