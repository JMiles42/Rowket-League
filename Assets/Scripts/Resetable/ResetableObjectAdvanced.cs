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

    [ContextMenu("Record Object")]
    public override void Record()
    {
        onRecord.Trigger();
    }

    [ContextMenu("Reset Object")]
    public override void Reset()
    {
        onReset.Trigger();
    }
}
