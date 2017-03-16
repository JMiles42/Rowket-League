using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "InputAI", menuName = "Rowket/Input/AI", order = 0)]
public class PlayerMotorInputAI : PlayerMotorInputBase
{
    public AiReactionTime ReactionTime;
    public const float ReactionStrength = 2000;
    public AiAggressiveMode AiMode;

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
                return Time.fixedTime;
            default:
                return 2f;
        }
    }

    public float GetReactionTime()
    {
        return GetReactionTime(ReactionTime);
    }

    public override void Enable(PlayerMotor callingObject)
    {
        callingObject.ActiveCoroutines.Add(callingObject.StartRoutine(AiUnique(callingObject)));
    }

    public override void Disable(PlayerMotor callingObject)
    {
        if (callingObject.ActiveCoroutines.Count <= 0) return;
        for (var i = callingObject.ActiveCoroutines.Count - 1; i > 0; i--)
        {
            callingObject.StopRoutine(callingObject.ActiveCoroutines[i]);
            callingObject.ActiveCoroutines.RemoveAt(i);
        }
        callingObject.ActiveCoroutines = new List<Coroutine>();
    }

    public override void Init(PlayerMotor callingObject)
    {
    }

    public override float GetMoveStrength()
    {
        return ReactionStrength;
    }

    IEnumerator AiUnique(PlayerMotor callingObject)
    {
        var ball = FindObjectOfType<Ball>();
        var finalRotation = Vector3.zero;
        var team = TeamManager.Instance.GetTeam(callingObject.myTeam);
        var goal = team.GetTeamsGoal();
        while (true)
        {
            yield return WaitForTimes.GetWaitForTime(GetReactionTime());

            var ballDist = Vector3.Distance(ball.Position, callingObject.Position);

            var BallToGoalDirection = GetDirection(goal.Position, ball.Position);
            var PlayerToGoalDirection = GetDirection(goal.Position, callingObject.Position);
            var PlayerToBallDirection = GetDirection(ball.Position, callingObject.Position);

            //Debug.Log(PlayerToGoalDirection);

            switch (AiMode)
            {
                case AiAggressiveMode.BallOnly:
                    //
                    //Aim for the ball no matter what
                    //
                    finalRotation = AimForObject(ball.Position, callingObject);
                    break;
                case AiAggressiveMode.Defensive:
                    //
                    //Make the Ai aim to keep the ball away from the goal while staying close to there goal
                    //
                    //TODO:Impliment Defensive AI
                    finalRotation = AimForObject(ball.Position, callingObject);
                    break;
                case AiAggressiveMode.GoalShooter:
                    //
                    //Make the Ai aim to get behind the ball then shoot for the goal
                    //
                    finalRotation = AimForObject(ball.Position - (BallToGoalDirection), callingObject);
                    //myRot = AimForObject((ball.Position + (-BallToGoalDirection * 2))+Vector3.up * 2, callingObject);
                    break;
                case AiAggressiveMode.BallFollower:
                    //
                    //Make the Ai aim to get behind the ball then shoot for the goal
                    //
                    finalRotation = AimForObject(
                        ball.Position + ((-BallToGoalDirection +
                                          (PlayerToBallDirection + PlayerToGoalDirection).normalized).normalized * 5),
                        callingObject);
                    break;
                case AiAggressiveMode.Aggressive:
                    //
                    //Aim for the closest none team mate
                    //
                    var otherMotor =
                        PlayerMotor.GetClosestMotor(callingObject.Position, callingObject, callingObject.myTeam);
                    var motorDist = Vector3.Distance(otherMotor.Position, callingObject.Position);
                    finalRotation = AimForObject(ballDist < motorDist ? ball.Position : otherMotor.Position, callingObject);
                    break;
                default:
                    //
                    //Default to just hitting the ball
                    //
                    finalRotation = AimForObject(ball.Position, callingObject);
                    break;
            }
            finalRotation.x /= 4;
            finalRotation.z /= 4;
            //var rot = callingObject.transform.TransformDirection(Quaternion.Euler(myRot) * callingObject.transform.forward * GetMoveStrength());
            callingObject.HitPuck(finalRotation * GetMoveStrength());
        }
    }

    private static Vector3 AimForObject(Vector3 target, PlayerMotor callingObject)
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

public enum AiAggressiveMode
{
    PlayerFour = -4,
    PlayerThree = -3,
    PlayerTwo = -2,
    PlayerOne = -1,
    BallOnly = 0,
    Defensive = 1,
    GoalShooter = 2,
    Aggressive = 3,
    BallFollower = 4,
}

public enum AiReactionTime
{
    Slow = 0,
    Normal = 1,
    Fast = 2,
    Broken = 3,
    Instant = 4
}