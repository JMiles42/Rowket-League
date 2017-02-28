using System;
using System.Collections;
using System.Collections.Generic;
using JMiles42.Data;
using UnityEngine;

public class TeamManager : Singleton<TeamManager>
{
    public List<TeamInstance> teams = new List<TeamInstance>();
    public Action onGoal;

    public TeamInstance BlueTeam
    {
        get { return teams[0]; }
        set { teams[0] = value; }
    }
    public TeamInstance RedTeam
    {
        get { return teams[1]; }
        set { teams[1] = value; }
    }

    private void OnEnable()
    {
        for (int i = 0, j = teams.Count; i < j; i++) teams[i].team.Enable();
    }
    private void OnDisable()
    {
        for (int i = 0, j = teams.Count; i < j; i++) teams[i].team.Disable();
    }
    public void GoalScored()
    {
        if(onGoal != null) onGoal();
    }
    private void Start()
    {
        for (int i = 0, j = teams.Count; i < j; i++) teams[i].team.score = 0;
    }

    public Team GetTeam(TeamType type)
    {
        for (int i = 0, j = teams.Count; i < j; i++)
        {
            if (teams[i].team.myTeam == type)
                return teams[i].team;
        }
        return null;
    }

    public TeamInstance.PlayerInstance GetPlayerInstance(PlayerMoter pM)
    {
        for (int i = 0, j = teams.Count; i < j; i++)
            for (int k = 0, l = teams[i].players.Count; i < j; i++)
                if (teams[i].players[k].player == pM) return teams[i].players[k];
        return null;
    }

    public void TeamInit()
    {
        for (int i = 0, j = teams.Count; i < j; i++) teams[i].team.StartListening();
    }
    public void TeamStop()
    {
        for (int i = 0, j = teams.Count; i < j; i++) teams[i].team.StopListening();
    }
    public void OnValidate()
    {
        for (int i = 0, j = teams.Count; i < j; i++)
            teams[i].name = teams[i].team.myTeam + " Team";
    }
    [Serializable]
    public class TeamInstance
    {
        public string name;
        public Team team;
        public List<PlayerInstance> players = new List<PlayerInstance>(4);

        [Serializable]
        public class PlayerInstance
        {
            public string name;
            public PlayerMoter player;
            public int Scores = 0;
            public int BallHits = 0;
        }
    }
}
