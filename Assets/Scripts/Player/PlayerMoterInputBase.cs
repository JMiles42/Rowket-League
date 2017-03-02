using System;
using UnityEngine;

public abstract class PlayerMoterInputBase : JMilesScriptableObject
{
    public abstract void Enable(PlayerMoter callingObject);
    public abstract void Disable(PlayerMoter callingObject);
    public abstract void Init(PlayerMoter callingObject);

    public abstract Vector3 GetMoveDirection();
    public abstract float GetMoveStrength();

    public virtual Vector3 GetMoveFinalDirection(Transform t)
    {
        var currentRotation = Quaternion.Euler(GetMoveDirection());
        return t.TransformDirection((currentRotation * t.forward) * GetMoveStrength());
    }
}
