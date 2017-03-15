using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResetableObjectBase : JMilesBehaviour, IResetable
{
    public static List<ResetableObjectBase> ResetableObjects = new List<ResetableObjectBase>();
    public abstract void Record();
    public abstract void Reset();

    protected virtual void OnEnable()
    {
        ResetableObjects.Add(this);
    }

    protected virtual void OnDisable()
    {
        ResetableObjects.Remove(this);
    }
}
