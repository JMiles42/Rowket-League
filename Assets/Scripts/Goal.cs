using System;
using UnityEngine;

public class Goal : JMilesBehaviour
{
    public TeamType myTeam;

    public Action onGoal;

    void GoalScored()
    {
        var ball = FindObjectOfType<Ball>();
        if (ball)
        {
            var playerInstance = TeamManager.Instance.GetPlayerInstance(ball.LastPlayerHit);
            playerInstance.ScoreGoal();
        }
        onGoal.Trigger();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Ball>())
            GoalScored();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
            GoalScored();
    }
}


