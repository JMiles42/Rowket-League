using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : JMilesRigidbodyBehaviour,IResetable
{
    private Vector3 startpos = Vector3.zero;
    private Quaternion startRot = new Quaternion();
    public void Record()
    {
        startpos = Position;
    }

    public void Reset()
    {
        Position = startpos;
        rigidbody.ResetVelocity();
    }

    // Use this for initialization
    void Start ()
	{
	    rigidbody.velocity = Vector3.down * 20f;
	}
}
