using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMoterInputBase : JMilesScriptableObject
{
    public abstract Vector3 GetInput();
    public abstract bool GetInputSubmit();
}
