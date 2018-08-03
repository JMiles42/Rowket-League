using System;
using UnityEngine;

/// <summary>
///     Detects when a ball enters then triggers the goal event
/// </summary>
public class Goal: JMilesBehaviour
{
	public TeamType myTeam;
	public Action   onGoal;

	private void GoalScored()
	{
		var ball = FindObjectOfType<Ball>();

		if(ball)
		{
			var playerInstance = TeamManager.Instance.GetPlayerInstance(ball.LastPlayerHit);
			playerInstance.ScoreGoal();
		}

		onGoal.Trigger();
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.GetComponent<Ball>())
			GoalScored();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<Ball>())
			GoalScored();
	}
}