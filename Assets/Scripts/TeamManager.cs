using System;
using System.Collections.Generic;
using JMiles42.Data;
using UnityEngine;

/// <summary>
/// Team manager, controls most of the team logic
/// </summary>
public class TeamManager : Singleton<TeamManager>
{
    public TeamInstance[] teams = {new TeamInstance("Pink"), new TeamInstance("Black")};
    public List<PlayerInstance> players = new List<PlayerInstance>();

    public TeamInstance TeamOne
    {
        get { return teams[0]; }
        set { teams[0] = value; }
    }

    public TeamInstance TeamTwo
    {
        get { return teams[1]; }
        set { teams[1] = value; }
    }

    private void OnEnable()
    {
        for (int i = 0, j = teams.Length; i < j; i++)
            teams[i].team.Enable();
    }

#if !UNITY_EDITOR
    void OnDisable()
    {
        for (int i = 0, j = teams.Length; i < j; i++)
            teams[i].team.Disable();
    }
#endif

    private void Start()
    {
        for (int i = 0, j = teams.Length; i < j; i++)
            teams[i].team.score = 0;
    }

    public Team GetTeam(TeamType type)
    {
        for (int i = 0, j = teams.Length; i < j; i++)
            if (teams[i].team.myTeam == type)
                return teams[i].team;
        return null;
    }

    public PlayerInstance GetPlayerInstance(PlayerMotor pM)
    {
        for (int i = 0, j = players.Count; i < j; i++)
            if (players[i].player == pM)
                return players[i];
        return null;
    }

    public List<int> GetTeamPlayersIndex(TeamType team)
    {
        var returnList = new List<int>();
        for (int i = 0, j = players.Count; i < j; i++)
            if (players[i].team == team)
                returnList.Add(i);
        return returnList;
    }

    public void TeamInit()
    {
        for (int i = 0, j = teams.Length; i < j; i++)
            teams[i].team.StartListening();
    }

    public void TeamStop()
    {
        for (int i = 0, j = teams.Length; i < j; i++)
            teams[i].team.StopListening();
    }

    [Serializable]
    public class TeamInstance
    {
        public string name;
        public Team team;
        public Material mat;

        public TeamInstance(string _name)
        {
            name = _name;
        }
    }

    [Serializable]
    public class PlayerInstance
    {
        [SerializeField] private PlayerMotor _player;

        public PlayerMotor player
        {
            get { return _player; }
            set { SetNewPlayerMotor(value); }
        }

        public TeamType team;
        public int Scores;
        public int BallHits;

        public PlayerInstance()
        {

        }

        public PlayerInstance(PlayerMotor __player)
        {
            _player = __player;
            team = _player.myTeam;
        }

        public void SetNewPlayerMotor(PlayerMotor __player)
        {
            _player = __player;
            team = _player.myTeam;
        }

        public void BallHit()
        {
            BallHits++;
        }

        public void ScoreGoal()
        {
            Scores++;
        }
    }
}