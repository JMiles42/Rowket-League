using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : JMilesBehaviour
{
    public TeamType myTeam;

    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }

    private void GoalScored()
    {
        StaticUnityEventManager.TriggerEvent("goal"+myTeam.ToString());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            var ball = other.gameObject.GetComponent<Ball>();
            GoalScored();
        }
    }
}


