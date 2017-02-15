using System;
using System.Collections;
using System.Collections.Generic;
using JMiles42.Data;
using UnityEngine;

public class TeamManager : Singleton<TeamManager>
{
    public Team TeamRed;
    public Team TeamBlue;

    private void Start()
    {
        TeamRed.score = 0;
        TeamBlue.score = 0;
    }

    public Team GetTeam(TeamType type)
    {
        switch (type)
        {
            case TeamType.Red:
                return TeamRed;
            case TeamType.Blue:
                return TeamBlue;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }
    }

    public void TeamInit()
    {
        TeamRed.StartListening();
        TeamBlue.StartListening();
    }
    public void TeamStop()
    {
        TeamRed.StopListening();
        TeamBlue.StopListening();
    }
}
