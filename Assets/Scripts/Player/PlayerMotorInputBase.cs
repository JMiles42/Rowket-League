/// <summary>
/// The base class to allow any inherited class to control the Motor
/// </summary>
public abstract class PlayerMotorInputBase : JMilesScriptableObject
{
    /// <summary>
    /// Used to enable the inputs control
    /// </summary>
    /// <param name="callingObject">What motor is this method affecting</param>
    public abstract void Enable(PlayerMotor callingObject);

    /// <summary>
    /// Used to disable the inputs control
    /// </summary>
    /// <param name="callingObject">What motor is this method affecting</param>
    public abstract void Disable(PlayerMotor callingObject);

    /// <summary>
    /// Used to initiate the inputs none controling aspects
    /// </summary>
    /// <param name="callingObject">What motor is this method affecting</param>
    public abstract void Init(PlayerMotor callingObject);

    /// <summary>
    /// Called by its self to get the strength
    /// </summary>
    /// <returns>Strength of movement</returns>
    public abstract float GetMoveStrength();

    /// <summary>
    /// Not used anymore
    /// </summary>
    /// <returns></returns>
    public abstract string GetName();
}