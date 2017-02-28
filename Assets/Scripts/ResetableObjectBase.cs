using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResetableObjectBase : JMilesBehaviour, IResetable
{
    public abstract void Record();
    public abstract void Reset();
}
