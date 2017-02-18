using System;
using UnityEngine;

public abstract class PlayerMoterInputBase : JMilesScriptableObject
{
    public abstract Vector3 GetMoveDirection();
    public abstract Vector2 GetInput();
    public abstract bool GetInputSubmit();
    public abstract float GetMoveStrength();
    public abstract void Enable(PlayerMoter callingObject);
    public abstract void Disable(PlayerMoter callingObject);

    public Action onLaunchPlayer;
}
