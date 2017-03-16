using JMiles42.Data;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedScoreWriter : Singleton<AdvancedScoreWriter>
{
    public Text RedTeamScore;
    public string RedTextString = "<color=red>Red Score</color>";

    public Text BlueTeamScore;
    public string BlueTextString = "<color=blue>Blue Score</color>";

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
        RedTeamScore.DOFade(value ? 1 : 0, fadeTime);
        BlueTeamScore.DOFade(value ? 1 : 0, fadeTime);
        Title.DOFade(value ? 1 : 0, fadeTime / 2);
    }

    void SetTextAlpha(float alpha)
    {
        var col = RedTeamScore.color;
        col.a = alpha;
        RedTeamScore.color = col;

        col = BlueTeamScore.color;
        col.a = alpha;
        BlueTeamScore.color = col;

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
        DisplayRedTeam();
        DisplayBlueTeam();
    }

    public void DisplayRedTeam()
    {
        var sb = new StringBuilder();
        sb.Append(RedTextString);
        sb.Append(": ");
        sb.Append(TeamManager.Instance.RedTeam.team.score);

        var RedTeam = TeamManager.Instance.GetTeamPlayersIndex(TeamType.Red);
        for (int i = 0, j = RedTeam.Count; i < j; i++)
            sb.AppendFormat(scoreEntryPerLine, TeamManager.Instance.players[RedTeam[i]].player.GetName(),
                TeamManager.Instance.players[RedTeam[i]].Scores);

        //Replaces custom line break due to Unity escaping the line breaks in editor
        RedTeamScore.text = sb.ToString().Replace("||n", "\n");
    }

    public void DisplayBlueTeam()
    {
        var sb = new StringBuilder();
        sb.Append(BlueTextString);
        sb.Append(": ");
        sb.Append(TeamManager.Instance.BlueTeam.team.score);
        var BlueTeam = TeamManager.Instance.GetTeamPlayersIndex(TeamType.Blue);
        for (int i = 0, j = BlueTeam.Count; i < j; i++)
            sb.AppendFormat(scoreEntryPerLine, TeamManager.Instance.players[BlueTeam[i]].player.GetName(),
                TeamManager.Instance.players[BlueTeam[i]].Scores);

        //Replaces custom line break due to Unity escaping the line breaks in editor
        BlueTeamScore.text = sb.ToString().Replace("||n", "\n");
    }
}