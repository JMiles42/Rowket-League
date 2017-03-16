public abstract class PlayerMoterInputBase : JMilesScriptableObject
{
    public abstract void Enable(PlayerMoter callingObject);
    public abstract void Disable(PlayerMoter callingObject);
    public abstract void Init(PlayerMoter callingObject);

    public abstract string GetName();

    public abstract float GetMoveStrength();
}
