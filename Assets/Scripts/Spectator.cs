using UnityEngine;

public class Spectator : JMilesBehaviour
{
    TeamType myTeam;
    public Renderer render;
    public Animator animator;

    const string cheer = "cheer";
    const string jump = "jump";

    void OnEnable()
    {
        //Choose my team
        myTeam = JMiles42.Maths.RandomBools.RandomBool() ? TeamType.Blue : TeamType.Red;
        //Set if my cheer animation is a jump
        animator.SetBool(jump, JMiles42.Maths.RandomBools.RandomBool());

        GameManager.Instance.onGameStartCountdown += StartCheer;
        GameManager.Instance.onGameStart += EndCheer;
        TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal().onGoal += StartCheer;

        //Set my color
        render.material = myTeam == TeamType.Blue ? TeamManager.Instance.BlueTeam.mat : TeamManager.Instance.RedTeam.mat;
    }

    void OnDisable()
    {
        GameManager.Instance.onGameStartCountdown -= StartCheer;
        GameManager.Instance.onGameStart -= EndCheer;
        TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal().onGoal -= StartCheer;
    }

    void StartCheer()
    {
        animator.SetBool(cheer, true);
    }

    void EndCheer()
    {
        animator.SetBool(cheer, false);
    }
}