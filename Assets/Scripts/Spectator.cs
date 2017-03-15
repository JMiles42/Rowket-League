using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Spectator : JMilesBehaviour
{
    TeamType myTeam;
    public Renderer render;
    public Animator anim;

    private const string chear = "chear";
    private const string jump = "jump";

    private void OnEnable()
    {
        myTeam = JMiles42.Maths.RandomBools.RandomBool() ? TeamType.Blue : TeamType.Red;

        anim.SetBool(jump, JMiles42.Maths.RandomBools.RandomBool());

        GameManager.Instance.onGameStartCountdown += StartChear;
        GameManager.Instance.onGameStart += EndChear;
        TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal().onGoal += StartChear;

        render.material = myTeam == TeamType.Blue
            ? TeamManager.Instance.BlueTeam.mat
            : TeamManager.Instance.RedTeam.mat;
    }

    private void OnDisable()
    {
        GameManager.Instance.onGameStartCountdown -= StartChear;
        GameManager.Instance.onGameStart -= EndChear;
        TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal().onGoal -= StartChear;
    }

    void StartChear()
    {
        anim.SetBool(chear, true);
    }

    void EndChear()
    {
        anim.SetBool(chear, false);
    }
}