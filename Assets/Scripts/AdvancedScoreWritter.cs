using JMiles42.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedScoreWritter : Singleton<AdvancedScoreWritter>
{
    public Text RedTeamScore;
    public string RedTextString = "<color=red>Red Score</color>";
    public Text BlueTeamScore;
    public string BlueTextString = "<color=blue>Blue Score</color>";
    public string scoreEntryPerLine = "||n<size=100>{0}: {1}</size>";
    public Text Title;
    public RenderTexture screenTexture;

    void OnEnable()
    {
        DisplayDeactivate();
        UpdateDisplay();

        if (!GameManager.Instance) return;
        GameManager.Instance.onGameStart += DisplayActivate;
        GameManager.Instance.onAnyGoal += UpdateDisplay;
        GameManager.Instance.onGameStart += UpdateDisplay;
    }

    void OnDisable()
    {
        if (!GameManager.Instance) return;
        GameManager.Instance.onGameStart -= DisplayActivate;
        GameManager.Instance.onAnyGoal -= UpdateDisplay;
        GameManager.Instance.onGameStart -= UpdateDisplay;
    }

    void ChangeDisplayActive(bool value = true)
    {
        RedTeamScore.gameObject.SetActive(value);
        BlueTeamScore.gameObject.SetActive(value);
        Title.gameObject.SetActive(value);
    }

    void DisplayDeactivate()
    {
        ChangeDisplayActive(false);
    }

    void DisplayActivate()
    {
        ChangeDisplayActive();
    }

    public void UpdateDisplay()
    {
        DisplayRedTeam();
        DisplayBlueTeam();
    }

    public void DisplayRedTeam()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(RedTextString);
        sb.Append(": ");
        sb.Append(TeamManager.Instance.RedTeam.team.score);

        var RedTeam = TeamManager.Instance.GetTeamPlayersIndecies(TeamType.Red);
        for (int i = 0, j = RedTeam.Count; i < j; i++)
            sb.AppendFormat(scoreEntryPerLine, TeamManager.Instance.players[RedTeam[i]].player.GetName(),
                TeamManager.Instance.players[RedTeam[i]].Scores);

        RedTeamScore.text = sb.ToString().Replace("||n", "\n");
    }

    public void DisplayBlueTeam()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(BlueTextString);
        sb.Append(": ");
        sb.Append(TeamManager.Instance.BlueTeam.team.score);
        var BlueTeam = TeamManager.Instance.GetTeamPlayersIndecies(TeamType.Blue);
        for (int i = 0, j = BlueTeam.Count; i < j; i++)
            sb.AppendFormat(scoreEntryPerLine, TeamManager.Instance.players[BlueTeam[i]].player.GetName(),
                TeamManager.Instance.players[BlueTeam[i]].Scores);

        BlueTeamScore.text = sb.ToString().Replace("||n", "\n");
    }
}