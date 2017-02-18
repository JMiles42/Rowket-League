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
        StopListening();
    }

    public void StartListening()
    {
        GetTeamsGoal().onGoal += TeamScored;
    }
    public void StopListening()
    {
#if !UNITY_EDITOR
        GetTeamsGoal().onGoal -= TeamScored;
#endif
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
