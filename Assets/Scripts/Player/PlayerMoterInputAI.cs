using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputAI", menuName = "Rowket/Input/AI", order = 0)]
public class PlayerMoterInputAI : PlayerMoterInputBase
{
    public Vector3 MoveDir;
    public float ReactionTime;
    public float ReactionStrength;
    public override Vector3 GetMoveDirection()
    {
        return MoveDir;
    }

    public override Vector2 GetInput()
    {
        return Vector3.zero;
    }

    public override bool GetInputSubmit()
    {
        return false;
    }

    public override void Enable(PlayerMoter callingObject)
    {
        callingObject.StartRoutine(PlayerUnique(callingObject));
    }

    public override void Disable(PlayerMoter callingObject)
    {
        callingObject.StopRoutine(PlayerUnique(callingObject));
    }

    public override float GetMoveStrength()
    {
        return ReactionStrength;
    }
    private IEnumerator PlayerUnique(PlayerMoter callingObject)
    {
        bool update = true;
        Ball ball = FindObjectOfType<Ball>();
        while (update)
        {
            //Stores resualt to sav multiple calls
            MoveDir = callingObject.transform.InverseTransformDirection(ball.Position + callingObject.Position);
            yield return WaitForTimes.GetWaitForTime(ReactionTime);
            if (onLaunchPlayer != null) onLaunchPlayer();
        }
    }
}
