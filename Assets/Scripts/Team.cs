using System;
using System.Linq;
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

#if !UNITY_EDITOR
    public void Disable()
    {
        StopListening();
    }
#endif

    public void StartListening()
    {
        GetTeamsGoal().onGoal += TeamScored;
    }

    public void StopListening()
    {
        GetTeamsGoal().onGoal -= TeamScored;
    }

    public void TeamScored()
    {
        score++;
    }

    public Goal GetTeamsGoal()
    {
        //Find all goal, then find the one that has the same team
        var goals = FindObjectsOfType<Goal>();
        return goals.FirstOrDefault(goal => goal.myTeam == myTeam);
    }
}

[Serializable]
public enum TeamType
{
    Red,
    Blue
}
