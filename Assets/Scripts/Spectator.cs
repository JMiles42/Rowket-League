using JMiles42.Maths;
using UnityEngine;

/// <summary>
///     Used for the spectators to trigger there animations and material colour
/// </summary>
public class Spectator: JMilesBehaviour
{
	private const string   cheer = "cheer";
	private const string   jump  = "jump";
	private       TeamType myTeam;
	public        Renderer render;
	public        Animator animator;

	private void OnEnable()
	{
		//Choose my team
		myTeam = RandomBools.RandomBool()? TeamType.TeamTwo : TeamType.TeamOne;
		//Set if my cheer animation is a jump
		animator.SetBool(jump, RandomBools.RandomBool());
		GameManager.Instance.onGameStartCountdown                  += StartCheer;
		GameManager.Instance.onGameStart                           += EndCheer;
		TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal().onGoal += StartCheer;

		//Set my color
		render.material = myTeam == TeamType.TeamTwo? TeamManager.Instance.TeamOne.mat : TeamManager.Instance.TeamTwo.mat;
	}

	private void OnDisable()
	{
#if !UNITY_EDITOR
        GameManager.Instance.onGameStartCountdown -= StartCheer;
        GameManager.Instance.onGameStart
 -= EndCheer;
        TeamManager.Instance.GetTeam(myTeam).GetTeamsGoal().onGoal
 -= StartCheer;
    #endif
	}

	private void StartCheer()
	{
		animator.SetBool(cheer, true);
	}

	private void EndCheer()
	{
		animator.SetBool(cheer, false);
	}
}