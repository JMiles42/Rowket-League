using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "InputAI", menuName = "Rowket/Input/AI", order = 0)]
public class PlayerMoterInputAI : PlayerMoterInputBase
{
    public AiReactionTime ReactionTime;
    public const float ReactionStrength = 2000;
    public AiAgressiveMode AiMode;

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

    public override void Enable(PlayerMoter callingObject)
    {
        //Debug.Log("Enable: " + callingObject);
        callingObject.ActiveCoroutines.Add(callingObject.StartRoutine(AiUnique(callingObject)));
    }

    public override void Disable(PlayerMoter callingObject)
    {
        if (callingObject.ActiveCoroutines.Count <= 0) return;
        for (int i = callingObject.ActiveCoroutines.Count - 1; i > 0; i--)
        {
            callingObject.StopRoutine(callingObject.ActiveCoroutines[i]);
            callingObject.ActiveCoroutines.RemoveAt(i);
        }
        callingObject.ActiveCoroutines = new List<Coroutine>();
    }

    public override void Init(PlayerMoter callingObject)
    {
    }

    public override float GetMoveStrength()
    {
        if (ReactionTime == AiReactionTime.Instant)
            return ReactionStrength / 4;
        return ReactionStrength;
    }

    private IEnumerator AiUnique(PlayerMoter callingObject)
    {
        bool update = true;
        var ball = FindObjectOfType<Ball>();
        var myRot = Vector3.zero;
        var team = TeamManager.Instance.GetTeam(callingObject.myTeam);
        var goal = team.GetTeamsGoal();
        while (update)
        {
            yield return WaitForTimes.GetWaitForTime(GetReactionTime());

            float ballDist = Vector3.Distance(ball.Position, callingObject.Position);

            var BallToGoalDirection = GetDirection(goal.Position, ball.Position);
            var PlayerToGoalDirection = GetDirection(goal.Position, callingObject.Position);
            var PlayerToBallDirection = GetDirection(ball.Position, callingObject.Position);

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
                    myRot = AimForObject(
                        ball.Position + ((-BallToGoalDirection +
                                          (PlayerToBallDirection + PlayerToGoalDirection).normalized).normalized * 5),
                        callingObject);
                    break;
                case AiAgressiveMode.Agressive:
                    //
                    //Aim for the closest none team mate
                    //
                    var otherMoter =
                        PlayerMoter.GetClosestMoter(callingObject.Position, callingObject, callingObject.myTeam);
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
            Debug.DrawLine(callingObject.Position, callingObject.Position + (myRot * 2));
            myRot.x /= 4;
            myRot.z /= 4;
            //var rot = callingObject.transform.TransformDirection(Quaternion.Euler(myRot) * callingObject.transform.forward * GetMoveStrength());
            callingObject.HitPuck(myRot * GetMoveStrength());
        }
    }

    private static Vector3 AimForObject(Vector3 target, PlayerMoter callingObject)
    {
        return (target - callingObject.Position).normalized;
    }

    private static Vector3 GetDirection(Vector3 target, Vector3 other)
    {
        return (target - other).normalized;
    }

    public override string GetName()
    {
        return GameManager.Instance.AiNames.Strings[Random.Range(0, GameManager.Instance.AiNames.Strings.Length)];
    }
}

public enum AiAgressiveMode
{
    PlayerTwo = -2,
    PlayerOne = -1,
    BallOnly = 0,
    Defensive = 1,
    GoalShooter = 2,
    Agressive = 3,
    BallFollower = 4
}

public enum AiReactionTime
{
    Slow = 0,
    Normal = 1,
    Fast = 2,
    Broken = 3,
    Instant = 4
}