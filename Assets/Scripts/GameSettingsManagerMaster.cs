using System;
using JMiles42.Data;
using UnityEngine;

/// <summary>
///     Controls the Game settings menu
/// </summary>
public class GameSettingsManagerMaster: Singleton<GameSettingsManagerMaster>
{
	public GameObject           SettingsPanel;
	public ButtonClickEvent     playGameButton;
	public SettingsMenuEntry[]  TeamOneEntries;
	public SettingsMenuEntry[]  TeamTwoEntries;
	public PlayerDetails[]      TeamOne;
	public PlayerDetails[]      TeamTwo;
	public PlayerFinalDetails[] TeamOneComposition;
	public PlayerFinalDetails[] TeamTwoComposition;

	private void OnEnable()
	{
		playGameButton.onMouseClick += PlayGame;
	}

	private void OnDisable()
	{
		playGameButton.onMouseClick -= PlayGame;
	}

	public void BuildArrays()
	{
		//Create the empty arrays
		TeamOne = new PlayerDetails[TeamOneEntries.Length];
		TeamTwo = new PlayerDetails[TeamTwoEntries.Length];

		//Store how many TeamOne team members are active
		var TeamTwoEnabled = 0;

		//
		for(int i = 0, j = TeamTwoEntries.Length; i < j; i++)
		{
			TeamTwo[i] = TeamTwoEntries[i].GetPlayerDetails();

			if(TeamTwo[i].Enabled)
				TeamTwoEnabled++;
		}

		//Store how many TeamTwo team members are active
		var TeamOneEnabled = 0;

		for(int i = 0, j = TeamOneEntries.Length; i < j; i++)
		{
			TeamOne[i] = TeamOneEntries[i].GetPlayerDetails();

			if(TeamOne[i].Enabled)
				TeamOneEnabled++;
		}

		//Setup the final arrays to there needed length
		TeamOneComposition = new PlayerFinalDetails[TeamOneEnabled];
		TeamTwoComposition = new PlayerFinalDetails[TeamTwoEnabled];

		//Setup the active players ready for the game manager to spawn them
		var currentIndex = 0;

		for(int i = 0, j = TeamOne.Length; i < j; i++)
		{
			if(TeamOne[i].Disabled)
				continue;

			TeamOneComposition[currentIndex] = new PlayerFinalDetails(i, GameManager.Instance.GetInputClass(TeamOne[i].AiMoterMode, TeamOne[i].aiReaction));
			currentIndex++;
		}

		currentIndex = 0;

		for(int i = 0, j = TeamTwo.Length; i < j; i++)
		{
			if(TeamTwo[i].Disabled)
				continue;

			TeamTwoComposition[currentIndex] = new PlayerFinalDetails(i, GameManager.Instance.GetInputClass(TeamTwo[i].AiMoterMode, TeamTwo[i].aiReaction));
			currentIndex++;
		}
	}

	private void PlayGame()
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
	public bool           IsPlayer;
	public InputMoterMode AiMoterMode;
	public AiReactionTime aiReaction;
	public string         Name;

	public PlayerDetails(bool isPlayer = false)
	{
		Name        = "";
		Enabled     = false;
		IsPlayer    = isPlayer;
		AiMoterMode = InputMoterMode.BallOnly;
		aiReaction  = AiReactionTime.Normal;
	}

	public PlayerDetails(AiReactionTime reaction, InputMoterMode moterMode, bool enabled, string name = "")
	{
		Name        = name;
		Enabled     = enabled;
		IsPlayer    = GameManager.GetIsPlayer(moterMode);
		AiMoterMode = moterMode;
		aiReaction  = reaction;
	}
}

[Serializable]
public struct PlayerFinalDetails
{
	public int                  detailsIndex;
	public PlayerMotorInputBase input;

	public PlayerFinalDetails(int pD, PlayerMotorInputBase pIB)
	{
		detailsIndex = pD;
		input        = pIB;
	}

	public static implicit operator int(PlayerFinalDetails pFD)
	{
		return pFD.detailsIndex;
	}

	public static implicit operator PlayerMotorInputBase(PlayerFinalDetails pFD)
	{
		return pFD.input;
	}
}