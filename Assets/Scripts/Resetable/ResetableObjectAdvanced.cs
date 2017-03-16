using System;
using UnityEngine;

/// <summary>
/// The Advanced Resetable Object, calls events when triggered, meaning any other class can subscribe to it
/// </summary>
public class ResetableObjectAdvanced : ResetableObjectBase
{
    public Action onRecord;
    public Action onReset;

    private void Start()
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