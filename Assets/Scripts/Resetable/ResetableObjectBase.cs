using System.Collections.Generic;

/// <summary>
///     The base class that all individual resetable object componets inheret from
/// </summary>
public abstract class ResetableObjectBase: JMilesBehaviour, IResetable
{
    /// <summary>
    ///     A list of every Resetable object, to make reseting all of them easy
    ///     TODO: see if Two static Actions would be better
    /// </summary>
    public static List<ResetableObjectBase> ResetableObjects = new List<ResetableObjectBase>();

    /// <summary>
    ///     Adds self to static list of ResetableObjects
    /// </summary>
    protected virtual void OnEnable()
	{
		ResetableObjects.Add(this);
	}

    /// <summary>
    ///     Removes self to static list of ResetableObjects
    /// </summary>
    protected virtual void OnDisable()
	{
		ResetableObjects.Remove(this);
	}

    /// <summary>
    ///     Resets all ResetableObjects
    /// </summary>
    public static void ResetAllObjects()
	{
		foreach(var obj in ResetableObjects)
			obj.Reset();
	}

    /// <summary>
    ///     Called to Record the data
    /// </summary>
    public abstract void Record();

    /// <summary>
    ///     Called to reset data to recorded state
    /// </summary>
    public abstract void Reset();
}