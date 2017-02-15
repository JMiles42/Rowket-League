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

    private void OnEnable()
    {
        StartListening();
    }

    private void OnDisable()
    {
        StopListening();
    }
    public void StartListening()
    {
        StaticUnityEventManager.StopListening("goal"+myTeam.ToString(), TeamScored);
        StaticUnityEventManager.StartListening("goal"+myTeam.ToString(), TeamScored);
    }
    public void StopListening()
    {
        StaticUnityEventManager.StopListening("goal"+myTeam.ToString(), TeamScored);
    }
    public void TeamScored()
    {
        score++;
    }

}

[Serializable]
public enum TeamType
{
    Red,
    Blue
}
