using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreDisplay : MonoBehaviour
{
    public TeamType myTeam;
    public Text myTextDisplay;

    private void Start()
    {
        if (myTextDisplay == null)
            myTextDisplay = GetComponent<Text>();
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
        StaticUnityEventManager.StartListening("goal"+myTeam.ToString(),UpdateDisplay);

    }
    public void EndUpListening()
    {
        StaticUnityEventManager.StopListening("goal"+myTeam.ToString(),UpdateDisplay);
    }

    private void UpdateDisplay()
    {
        Team team = TeamManager.Instance.GetTeam(myTeam);
        myTextDisplay.text = string.Format("{0}",team.score);
        myTextDisplay.color = team.myTeamColour;
    }
}
