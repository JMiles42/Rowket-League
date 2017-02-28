using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetableObjectAdvanced : ResetableObjectBase
{
    public Action onRecord;
    public Action onReset;

    void Start()
    {
        Record();
    }
    void OnEnable()
    {
        Record();
    }
    public override void Record()
    {
        if (onRecord != null) onRecord();
    }

    public override void Reset()
    {
        if (onReset != null) onReset();
    }
}
