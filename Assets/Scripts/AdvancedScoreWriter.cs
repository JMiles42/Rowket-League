using JMiles42.Data;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedScoreWriter : Singleton<AdvancedScoreWriter>
{
    public Text TeamOneScore;
    public string TeamOneString = "<color=red>Red Score</color>";

    public Text TeamTwoScore;
    public string TeamTwoString = "<color=blue>Blue Score</color>";

    public string scoreEntryPerLine = "||n<size=100>{0}: {1}</size>";
    public Text Title;
    public RenderTexture screenTexture;
    public float fadeTime = 1;
    void OnEnable()
    {
        SetTextAlpha(0);
        GameManager.Instance.onGameStart += DisplayActivate;
        GameManager.Instance.onAnyGoal += UpdateDisplay;
        GameManager.Instance.onGameStart += UpdateDisplay;
        DisplayDeactivate();
    }

    void OnDisable()
    {
        GameManager.Instance.onGameStart -= DisplayActivate;
        GameManager.Instance.onAnyGoal -= UpdateDisplay;
        GameManager.Instance.onGameStart -= UpdateDisplay;
    }

    void ChangeDisplayActive(bool value = true)
    {
        TeamOneScore.DOFade(value ? 1 : 0, fadeTime);
        TeamTwoScore.DOFade(value ? 1 : 0, fadeTime);
        Title.DOFade(value ? 1 : 0, fadeTime / 2);
    }

    void SetTextAlpha(float alpha)
    {
        var col = TeamOneScore.color;
        col.a = alpha;
        TeamOneScore.color = col;

        col = TeamTwoScore.color;
        col.a = alpha;
        TeamTwoScore.color = col;

        col = Title.color;
        col.a = alpha;
        Title.color = col;
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
        DisplayTeamOne();
        DisplayTeamTwo();
    }

    public void DisplayTeamOne()
    {
        var sb = new StringBuilder();
        sb.Append(TeamOneString);
        sb.Append(": ");
        sb.Append(TeamManager.Instance.RedTeam.team.score);

        var RedTeam = TeamManager.Instance.GetTeamPlayersIndex(TeamType.Red);

        //Adds each players name & score to the StringBuilder
        for (int i = 0, j = RedTeam.Count; i < j; i++)
            sb.AppendFormat(scoreEntryPerLine, TeamManager.Instance.players[RedTeam[i]].player.GetName(),
                TeamManager.Instance.players[RedTeam[i]].Scores);

        //Replaces custom line break due to Unity escaping the line breaks in editor
        TeamOneScore.text = sb.ToString().Replace("||n", "\n");
    }

    public void DisplayTeamTwo()
    {
        var sb = new StringBuilder();
        sb.Append(TeamTwoString);
        sb.Append(": ");
        sb.Append(TeamManager.Instance.BlueTeam.team.score);
        var BlueTeam = TeamManager.Instance.GetTeamPlayersIndex(TeamType.Blue);

        //Adds each players name & score to the StringBuilder
        for (int i = 0, j = BlueTeam.Count; i < j; i++)
            sb.AppendFormat(scoreEntryPerLine, TeamManager.Instance.players[BlueTeam[i]].player.GetName(),
                TeamManager.Instance.players[BlueTeam[i]].Scores);

        //Replaces custom line break due to Unity escaping the line breaks in editor
        TeamTwoScore.text = sb.ToString().Replace("||n", "\n");
    }
}