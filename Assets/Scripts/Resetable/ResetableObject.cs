using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetableObject : ResetableObjectBase
{
    public static List<ResetableObject> ResetableObjects = new List<ResetableObject>();
    public Transform transformToReset;
    public Vector3 Pos;
    public Quaternion Rot;

    void Start()
    {
        if (transformToReset == null) transformToReset = transform;
        Record();
    }

    void OnEnable()
    {
        ResetableObjects.Add(this);
        if (transformToReset == null) transformToReset = transform;
        Record();
    }

    void OnDisable()
    {
        ResetableObjects.Remove(this);
    }

    public override void Record()
    {
        Pos = transformToReset.position;
        Rot = transformToReset.rotation;
    }

    public override void Reset()
    {
        transformToReset.position = Pos;
        transformToReset.rotation = Rot;
        if (transformToReset.GetComponent<Rigidbody>())
            transformToReset.GetComponent<Rigidbody>().ResetVelocity();
    }
}
