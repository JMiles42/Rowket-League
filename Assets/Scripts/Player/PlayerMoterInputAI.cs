using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputAI", menuName = "Rowket/Input/AI", order = 0)]
public class PlayerMoterInputAI : PlayerMoterInputBase
{
    public AiReactionTime ReactionTime;
    public const float ReactionStrength = 2000;
    public AiAgressiveMode AiMode;

    public Vector3 MoveDir;
    public static float GetReactionTime(AiReactionTime reaction)
    {
        switch (reaction)
        {
            case AiReactionTime.Slow:
                return 2f;
            case AiReactionTime.Normal:
                return 1.5f;
            case AiReactionTime.Fast:
                return 1f;
            case AiReactionTime.Broken:
                return 0.5f;
            case AiReactionTime.Instant:
                return 0.001f;
            default:
                return 2f;
        }
    }

    public float GetReactionTime()
    {
        return GetReactionTime(ReactionTime);
    }

    public override Vector3 GetMoveDirection()
    {
        return MoveDir;
    }

    public override Vector3 GetMoveFinalDirection(Transform t)
    {
        return Vector3.zero;//MoveDir * GetMoveStrength();
    }

    public override void Enable(PlayerMoter callingObject)
    {
        callingObject.StartRoutine(AiUnique(callingObject));
    }

    public override void Disable(PlayerMoter callingObject)
    {
        callingObject.StopRoutine(AiUnique(callingObject));
    }

    public override void Init(PlayerMoter callingObject)
    {

    }

    public override float GetMoveStrength()
    {
        if(ReactionTime == AiReactionTime.Instant)
            return ReactionStrength/4;
        return ReactionStrength;
    }

    private IEnumerator AiUnique(PlayerMoter callingObject)
    {
        bool update = true;
        Ball ball = FindObjectOfType<Ball>();
        Vector3 myRot = Vector3.zero;
        Team team = TeamManager.Instance.GetTeam(callingObject.myTeam);
        Goal goal = team.GetTeamsGoal();
        float number = UnityEngine.Random.value;
        while (update)
        {
            //Debug.LogWarning(number);
            Debug.LogWarning("Start: "+callingObject);
            Debug.LogWarning(callingObject.onLaunchPlayer);
            yield return WaitForTimes.GetWaitForTime(GetReactionTime());

            float ballDist = Vector3.Distance(ball.Position, callingObject.Position);

            Vector3 BallToGoalDirection = GetDirection(goal.Position, ball.Position);
            Vector3 PlayerToGoalDirection = GetDirection(goal.Position, callingObject.Position);
            Vector3 PlayerToBallDirection = GetDirection(ball.Position, callingObject.Position);

            //Debug.Log(PlayerToGoalDirection);

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
                    //TODO:Impliment Defensive AI
                    myRot = AimForObject(ball.Position, callingObject);
                    break;
                case AiAgressiveMode.GoalShooter:
                    // 
                    //Make the Ai aim to get behind the ball then shoot for the goal
                    //

                    myRot = AimForObject(ball.Position - (BallToGoalDirection), callingObject);
                    //myRot = AimForObject((ball.Position + (-BallToGoalDirection * 2))+Vector3.up * 2, callingObject);
                    break;
                case AiAgressiveMode.BallFollower:
                    // 
                    //Make the Ai aim to get behind the ball then shoot for the goal
                    // 
                    myRot = AimForObject(ball.Position + ((-BallToGoalDirection + (PlayerToBallDirection + PlayerToGoalDirection).normalized).normalized * 5), callingObject);
                    break;
                case AiAgressiveMode.Agressive:
                    //
                    //Aim for the closest none team mate
                    //
                    var otherMoter = PlayerMoter.GetClosestMoter(callingObject.Position, callingObject, callingObject.myTeam);
                    var moterDist = Vector3.Distance(otherMoter.Position, callingObject.Position);

                    myRot = AimForObject(ballDist < moterDist ? ball.Position : otherMoter.Position, callingObject);
                    break;
                default:
                    //
                    //Defualt to just hitting the ball
                    //
                    myRot = AimForObject(ball.Position, callingObject);
                    break;
            }
            //MoveDir = myRot = new Vector3(0,myRot.y,0);
            MoveDir.Normalize();
            MoveDir.y = 0;
            Debug.LogWarning("End: "+callingObject);
            Debug.DrawLine(callingObject.Position, callingObject.Position+(MoveDir *2));
            callingObject.HitPuck(MoveDir * GetMoveStrength());
            //if (callingObject.onLaunchPlayer != null) callingObject.onLaunchPlayer(GetMoveFinalDirection(callingObject.transform));
            //MoveDir = myRot * GetMoveStrength();
        }
    }

    private Vector3 AimForObject(Vector3 target, PlayerMoter callingObject)
    {
        return (target - callingObject.Position).normalized;
    }
    private Vector3 GetDirection(Vector3 target, Vector3 other)
    {
        return (target - other).normalized;
    }
}

public enum AiAgressiveMode
{
    BallOnly,
    Defensive,
    GoalShooter,
    Agressive,
    BallFollower
}

public enum AiReactionTime
{
    Slow,
    Normal,
    Fast,
    Broken,
    Instant
}
