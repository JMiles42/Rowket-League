using UnityEngine;

public class Spectator : JMilesBehaviour
{
    TeamType myTeam;
    public Renderer render;
    public Animator animator;

    const string chear = "chear";
    const string jump = "jump";

    void OnEnable()
    {
        myTeam = JMiles42.Maths.RandomBools.RandomBool() ? TeamType.Blue : TeamType.Red;

        animator.SetBool(jump, JMiles42.Maths.RandomBools.RandomBool());

        GameManager.Instance.onGameStartCountdown += StartChear;
        GameManager.Instance.onGameStart += EndChear;
        TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal().onGoal += StartChear;

        render.material = myTeam == TeamType.Blue ? TeamManager.Instance.BlueTeam.mat : TeamManager.Instance.RedTeam.mat;
    }

    void OnDisable()
    {
        GameManager.Instance.onGameStartCountdown -= StartChear;
        GameManager.Instance.onGameStart -= EndChear;
        TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal().onGoal -= StartChear;
    }

    void StartChear()
    {
        animator.SetBool(chear, true);
    }

    void EndChear()
    {
        animator.SetBool(chear, false);
    }
}