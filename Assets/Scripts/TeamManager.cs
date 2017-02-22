using System;
using System.Collections;
using System.Collections.Generic;
using JMiles42.Data;
using UnityEngine;

public class TeamManager : Singleton<TeamManager>
{
    public List<Team> teams = new List<Team>();
    public Action onGoal;

    private void OnEnable()
    {
        foreach (var team in teams) team.Enable();
    }
    private void OnDisable()
    {
        foreach (var team in teams) team.Disable();
    }
    public void GoalScored()
    {
        if(onGoal != null) onGoal();
    }
    private void Start()
    {
        foreach (var team in teams) team.score = 0;
    }

    public Team GetTeam(TeamType type)
    {
        foreach (var team in teams)
            if (team.myTeam == type)
                return team;
        return null;
    }

    public void TeamInit()
    {
        foreach (var team in teams)
            team.StartListening();
    }
    public void TeamStop()
    {
        foreach (var team in teams)
            team.StopListening();
    }
}
