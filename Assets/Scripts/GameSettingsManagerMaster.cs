using System;
using System.Collections;
using System.Collections.Generic;
using JMiles42.Data;
using UnityEngine;

public class GameSettingsManagerMaster : Singleton<GameSettingsManagerMaster>
{
    public GameObject SettingsPanel;
    public ButtonClickEvent playGameButton;

    public AiPlayerMenuEntry[] RedEntries;
    public AiPlayerMenuEntry[] BlueEntries;

    public PlayerDetails[] RedTeam;
    public PlayerDetails[] BlueTeam;

    public PlayerFinalDetails[] RedTeamComposition;
    public PlayerFinalDetails[] BlueTeamComposition;

    void OnEnable()
    {
        playGameButton.onMouseClick += PlayGame;
    }

    void OnDisable()
    {
        playGameButton.onMouseClick -= PlayGame;
    }

    public void BuildArrays()
    {
        //Create the empty arrays
        RedTeam = new PlayerDetails[RedEntries.Length];
        BlueTeam = new PlayerDetails[BlueEntries.Length];

        //Store how many Red team members are active
        var EnabledRed = 0;
        //
        for (int i = 0, j = RedEntries.Length; i < j; i++)
        {
            RedTeam[i] = RedEntries[i].GetPlayerDetails();
            if (RedTeam[i].Enabled)
                EnabledRed++;
        }

        //Store how many Blue team members are active
        var EnabledBlue = 0;
        for (int i = 0, j = BlueEntries.Length; i < j; i++)
        {
            BlueTeam[i] = BlueEntries[i].GetPlayerDetails();
            if (BlueTeam[i].Enabled)
                EnabledBlue++;
        }

        //Setup the final arrays to there needed length
        RedTeamComposition = new PlayerFinalDetails[EnabledRed];
        BlueTeamComposition = new PlayerFinalDetails[EnabledBlue];


        //Setup the active players ready for the game manager to spawn them
        var currentIndex = 0;
        for (int i = 0, j = RedTeam.Length; i < j; i++)
        {
            if (RedTeam[i].Disabled) continue;
            RedTeamComposition[currentIndex] = new PlayerFinalDetails(i, GameManager.Instance.GetInputClass(RedTeam[i].aiMode, RedTeam[i].aiReaction));
            currentIndex++;
        }
        currentIndex = 0;
        for (int i = 0, j = BlueTeam.Length; i < j; i++)
        {
            if (BlueTeam[i].Disabled) continue;
            BlueTeamComposition[currentIndex] = new PlayerFinalDetails(i, GameManager.Instance.GetInputClass(BlueTeam[i].aiMode, BlueTeam[i].aiReaction));
            currentIndex++;
        }
    }

    void PlayGame()
    {
        SettingsPanel.SetActive(false);
    }
}

[Serializable]
public struct PlayerDetails
{
    public bool Enabled;

    public bool Disabled
    {
        get { return !Enabled; }
    }

    public bool IsPlayer;
    public AiAggressiveMode aiMode;
    public AiReactionTime aiReaction;
    public string Name;

    public PlayerDetails(bool isPlayer = false)
    {
        Name = "";
        Enabled = false;
        IsPlayer = isPlayer;
        aiMode = AiAggressiveMode.BallOnly;
        aiReaction = AiReactionTime.Normal;
    }

    public PlayerDetails(AiReactionTime reaction, AiAggressiveMode mode, bool enabled, bool isPlayer = false,
        string name = "")
    {
        Name = name;
        Enabled = enabled;
        IsPlayer = isPlayer;
        aiMode = mode;
        aiReaction = reaction;
    }
}

[Serializable]
public struct PlayerFinalDetails
{
    public int detailsIndex;
    public PlayerMoterInputBase input;

    public PlayerFinalDetails(int pD, PlayerMoterInputBase pIB)
    {
        detailsIndex = pD;
        input = pIB;
    }

    public static implicit operator int(PlayerFinalDetails pFD)
    {
        return pFD.detailsIndex;
    }

    public static implicit operator PlayerMoterInputBase(PlayerFinalDetails pFD)
    {
        return pFD.input;
    }
}