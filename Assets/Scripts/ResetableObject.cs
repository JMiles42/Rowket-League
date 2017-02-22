using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetableObject : JMilesBehaviour, IResetable
{
    public Vector3 Pos;
    public Quaternion Rot;

    void Start()
    {
        Record();
    }
    void OnEnable()
    {
        Record();
    }
    public void Record()
    {
        Pos = Position;
        Rot = Rotation;
    }

    public void Reset()
    {
        Position = Pos;
        Rotation = Rot;
    }
}
