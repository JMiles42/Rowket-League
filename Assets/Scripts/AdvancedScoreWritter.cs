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
    public string scoreEntryPerLine = "||n{0}: {1}";
    public Text Title;
    public RenderTexture screenTexture;

    void OnEnable()
    {
        DisplayDeactivate();
        UpdateDisplay();
        GameManager.Instance.onGameStart += DisplayActivate;
        TeamManager.Instance.onGoal += UpdateDisplay;
    }

    void OnDisable()
    {
        GameManager.Instance.onGameStart -= DisplayActivate;
        TeamManager.Instance.onGoal -= UpdateDisplay;
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
        ChangeDisplayActive(true);
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

        for (int i = 0, j = TeamManager.Instance.RedTeam.players.Count; i < j; i++)
            if (TeamManager.Instance.RedTeam.players[i].player.gameObject.activeInHierarchy)
                sb.AppendFormat(scoreEntryPerLine, TeamManager.Instance.RedTeam.players[i].name, TeamManager.Instance.RedTeam.players[i].Scores);

        RedTeamScore.text = sb.ToString().Replace("||n","\n");
    }
    public void DisplayBlueTeam()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(BlueTextString);
        sb.Append(": ");
        sb.Append(TeamManager.Instance.BlueTeam.team.score);

        for (int i = 0, j = TeamManager.Instance.BlueTeam.players.Count; i < j; i++)
            if (TeamManager.Instance.BlueTeam.players[i].player.gameObject.activeInHierarchy)
                sb.AppendFormat(scoreEntryPerLine, TeamManager.Instance.BlueTeam.players[i].name, TeamManager.Instance.BlueTeam.players[i].Scores);

        BlueTeamScore.text = sb.ToString().Replace("||n", "\n");
    }
}
