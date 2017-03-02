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

    public static readonly string[] Names =
    {
        "Ari","Miu","Fred","Jordan","Kim","Ravi",
        "Bob","Gayoung","Minhee","Hyoeun","Jeonyoul",
        "Jae","Gary","Ji Hyo","Haha","Eunice","Huihyeon",
        "Jenny","Yebin","Eunjin","Chaeyeon","Eunchae",
        "Exy","Jin","Suga","J-Hope","Rap Monster",
        "Jimin","V","Jungkook","Tae-il", "B-Bomb","Jae-hyo",
        "U-Kwon","Park Kyung","Zico","P.O","Xiumin","Suho",
        "Lay","Baekhyun","Chen","Chanyeol","D.O.","Kai",
        "Sehun","J.Seph","B.M","Somin","Jiwoo","Matthew",
        "James","Lenard","Karissa","Sunny","Karena","Ronnie",
        "Reed","Herbert","Stewart","Jaye","Araceli","Carmelita",
        "Beverly","Chere","Mirta","Kasie","Deja","Shizue",
        "Jacquelyne","Randy","Bobby","Gabriel","Lindsey",
        "Gregg","Mitchel","Albert","Kirk","Berry","Aurelio",
        "Johnathon","Abraham","Dusty","Anton","Garry","Chang",
        "Rodger","Shane","Hiram","Herb","Lucas",
    };

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
        Debug.Log("Enable: " + callingObject);
        callingObject.ActiveCoroutines.Add(callingObject.StartRoutine(AiUnique(callingObject)));
    }

    public override void Disable(PlayerMoter callingObject)
    {
        if (callingObject.ActiveCoroutines.Count > 0)
        {
            for (int i = callingObject.ActiveCoroutines.Count-1; i > 0; i--)
            {
                callingObject.StopRoutine(callingObject.ActiveCoroutines[i]);
                callingObject.ActiveCoroutines.RemoveAt(i);
            }
            callingObject.ActiveCoroutines = new List<Coroutine>();
        }
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
        Ball ball = FindObjectOfType<Ball>();
        Vector3 myRot = Vector3.zero;
        Team team = TeamManager.Instance.GetTeam(callingObject.myTeam);
        Goal goal = team.GetTeamsGoal();
        while (update)
        {
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
            Debug.DrawLine(callingObject.Position, callingObject.Position + (myRot * 2));
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
        return Names[Random.Range(0,Names.Length)];
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
