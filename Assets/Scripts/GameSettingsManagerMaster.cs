using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManagerMaster : JMilesBehaviour
{
    public ButtonClickEvent playGameButton;
    public AiPlayerMenuEntry[] RedEntries;
    public PlayerDetails[] RedTeam;
    public AiPlayerMenuEntry[] BlueEntries;
    public  PlayerDetails[] BlueTeam;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
}

[Serializable]
public struct PlayerDetails
{
    public bool Enabled;
    public bool IsPlayer;
    public AiAgressiveMode aiMode;
    public AiReactionTime aiReaction;

    public PlayerDetails(bool isPlayer = false)
    {
        Enabled = false;
        IsPlayer = isPlayer;
        aiMode = AiAgressiveMode.BallOnly;
        aiReaction = AiReactionTime.Normal;
    }
    public PlayerDetails(AiReactionTime reaction, AiAgressiveMode mode,bool isPlayer = false)
    {
        Enabled = false;
        IsPlayer = isPlayer;
        aiMode = mode;
        aiReaction = reaction;
    }

}