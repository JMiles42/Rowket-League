using UnityEngine;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{
    public TeamType myTeam;
    public Text myTextDisplay;

    private void Start()
    {
        if (myTextDisplay == null) myTextDisplay = GetComponent<Text>();
        UpdateDisplay();
    }

    private void OnEnable()
    {
        SetUpListening();
    }

    private void OnDisable()
    {
        EndUpListening();
    }

    public void SetUpListening()
    {
        if (!TeamManager.Instance) return;
        EndUpListening();
        var goal = TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal();
        if (goal) goal.onGoal += UpdateDisplay;
    }
    public void EndUpListening()
    {
        if (!TeamManager.Instance) return;
        var goal = TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal();
        if (goal) goal.onGoal -= UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        Team team = TeamManager.Instance.GetTeam(myTeam);
        myTextDisplay.text = string.Format("{0}", team.score);
        myTextDisplay.color = team.myTeamColour;
    }
}
