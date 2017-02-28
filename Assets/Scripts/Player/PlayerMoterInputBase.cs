using System;
using UnityEngine;

public abstract class PlayerMoterInputBase : JMilesScriptableObject
{
    public abstract Vector3 GetMoveDirection();

    public virtual Vector3 GetMoveFinalDirection(Transform t)
    {
        var currentRotation = Quaternion.Euler(GetMoveDirection());
        return t.TransformDirection((currentRotation * t.forward) * GetMoveStrength());
    }

    public abstract float GetMoveStrength();
    public abstract void Enable(PlayerMoter callingObject);
    public abstract void Disable(PlayerMoter callingObject);

    public Action onLaunchPlayer;
}
