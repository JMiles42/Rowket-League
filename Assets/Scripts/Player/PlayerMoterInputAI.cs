using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputAI", menuName = "Rowket/Input/AI", order = 0)]
public class PlayerMoterInputAI : PlayerMoterInputBase
{
    public Vector3 MoveDir;
    public float ReactionTime;
    public float ReactionStrength;
    public AiAgressiveMode AiMode;

    public override Vector3 GetMoveDirection()
    {
        return MoveDir;
    }

    public override Vector3 GetMoveFinalDirection(Transform t)
    {
        return MoveDir * GetMoveStrength();
    }

    public override void Enable(PlayerMoter callingObject)
    {
        callingObject.StartRoutine(AiUnique(callingObject));
    }

    public override void Disable(PlayerMoter callingObject)
    {
        callingObject.StopRoutine(AiUnique(callingObject));
    }

    public override float GetMoveStrength()
    {
        return ReactionStrength;
    }
    private IEnumerator AiUnique(PlayerMoter callingObject)
    {
        bool update = true;
        Ball ball = FindObjectOfType<Ball>();
        Vector3 myRot = Vector3.zero;
        while (update)
        {
            yield return WaitForTimes.GetWaitForTime(ReactionTime);
            var ballDist = Vector3.Distance(ball.Position, callingObject.Position);
            switch (AiMode)
            {
                case AiAgressiveMode.BallOnly:
                    //
                    //Aim for the ball no matter what
                    //
                    myRot = AimForObject(ball.Position, callingObject);
                    break;
                case AiAgressiveMode.Defensive:
                    //
                    //Make the Ai aim to keep the ball away from the goal while staying close to there goal
                    //
                    myRot = AimForObject(ball.Position, callingObject);
                    break;
                case AiAgressiveMode.GoalShooter:
                    // 
                    //Make the Ai aim to get behind the ball then shoot for the goal
                    // 
                    myRot = AimForObject(ball.Position, callingObject);
                    if (ballDist > 5)
                        MoveDir = MoveDir * -2f;
                    break;
                case AiAgressiveMode.Agressive:
                    //
                    //Aim for the closest none team mate
                    //
                    var otherMoter = PlayerMoter.GetClosestMoter(callingObject.Position, callingObject,callingObject.myTeam);
                    var moterDist = Vector3.Distance(otherMoter.Position, callingObject.Position);

                    myRot = AimForObject(ballDist < moterDist ? ball.Position : otherMoter.Position, callingObject);
                    break;
                default:
                    //
                    //Defualt to just hitting the ball
                    //
                    myRot = AimForObject(ball.Position,callingObject);
                    break;
            }
            MoveDir = myRot;
            if (onLaunchPlayer != null) onLaunchPlayer();
        }
    }

    private Vector3 AimForObject(Vector3 target, PlayerMoter callingObject)
    {
        return MoveDir = (target - callingObject.Position).normalized;
    }
}

public enum AiAgressiveMode
{
    BallOnly,
    Defensive,
    GoalShooter,
    Agressive
}
