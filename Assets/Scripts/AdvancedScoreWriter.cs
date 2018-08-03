using System.Text;
using DG.Tweening;
using JMiles42.Data;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Used to display both teams to the scoreboard
/// </summary>
public class AdvancedScoreWriter: Singleton<AdvancedScoreWriter>
{
	public Text          TeamOneScore;
	public string        TeamOneString = "<color=red>TeamOne Score</color>";
	public Text          TeamTwoScore;
	public string        TeamTwoString     = "<color=blue>TeamTwo Score</color>";
	public string        scoreEntryPerLine = "||n<size=100>{0}: {1}</size>";
	public Text          Title;
	public RenderTexture screenTexture;
	public float         fadeTime = 1;

	private void OnEnable()
	{
		SetTextAlpha(0);
		GameManager.Instance.onGameStart += DisplayActivate;
		GameManager.Instance.onAnyGoal   += UpdateDisplay;
		GameManager.Instance.onGameStart += UpdateDisplay;
		DisplayDeactivate();
	}

	private void OnDisable()
	{
#if !UNITY_EDITOR
        GameManager.Instance.onGameStart -= DisplayActivate;
        GameManager.Instance.onAnyGoal -= UpdateDisplay;
        GameManager.Instance.onGameStart -= UpdateDisplay;
#endif
	}

	private void ChangeDisplayActive(bool value = true)
	{
		TeamOneScore.DOFade(value? 1 : 0, fadeTime);
		TeamTwoScore.DOFade(value? 1 : 0, fadeTime);
		Title.DOFade(value? 1 : 0, fadeTime / 2);
	}

	private void SetTextAlpha(float alpha)
	{
		var col = TeamOneScore.color;
		col.a              = alpha;
		TeamOneScore.color = col;
		col                = TeamTwoScore.color;
		col.a              = alpha;
		TeamTwoScore.color = col;
		col                = Title.color;
		col.a              = alpha;
		Title.color        = col;
	}

	private void DisplayDeactivate()
	{
		ChangeDisplayActive(false);
	}

	private void DisplayActivate()
	{
		ChangeDisplayActive();
	}

	public void UpdateDisplay()
	{
		DisplayTeam(TeamType.TeamOne, TeamOneScore);
		DisplayTeam(TeamType.TeamTwo, TeamTwoScore);
	}

	private void DisplayTeam(TeamType teamType, Text text)
	{
		var sb = new StringBuilder();

		//Add the very team specific text to the stringbuilder
		switch(teamType)
		{
			case TeamType.TeamOne:
				sb.Append(TeamOneString);
				sb.Append(": ");
				sb.Append(TeamManager.Instance.TeamOne.team.score);

				break;
			case TeamType.TeamTwo:
				sb.Append(TeamTwoString);
				sb.Append(": ");
				sb.Append(TeamManager.Instance.TeamTwo.team.score);

				break;
		}

		//Get the indeices of teamTypes team, to itterate the master array
		var TeamIndecies = TeamManager.Instance.GetTeamPlayersIndex(teamType);

		//Adds each players name & score to the StringBuilder
		//Using the scoreEntryPerLine config option
		for(int i = 0, j = TeamIndecies.Count; i < j; i++)
		{
			sb.AppendFormat(scoreEntryPerLine, TeamManager.Instance.players[TeamIndecies[i]].player.GetName(), TeamManager.Instance.players[TeamIndecies[i]].Scores);
		}

		//Replaces custom line break due to Unity escaping the line breaks in editor
		//Tried to keep similar syntax
		text.text = sb.ToString().Replace("||n", "\n");
	}
}