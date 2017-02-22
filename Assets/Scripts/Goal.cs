using System;
using UnityEngine;

public class Goal : JMilesBehaviour
{
    public TeamType myTeam;

    public Action onGoal;

    private void GoalScored()
    {
        if (onGoal != null) onGoal();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            //var ball = other.gameObject.GetComponent<Ball>();
            GoalScored();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            //var ball = other.gameObject.GetComponent<Ball>();
            GoalScored();
        }
    }
}


