using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMiles42.Data;
[RequireComponent(typeof(ResetableObjectAdvanced))]
public class Ball : SingletonRigidbody<Ball>, IResetable
{
    ResetableObjectAdvanced resetableObjectAdvanced;
    private Vector3 startPos = Vector3.zero;
    private Quaternion startRot = new Quaternion();
    public PlayerMoter LastPlayerHit;


    void OnEnable()
    {
        resetableObjectAdvanced = GetComponent<ResetableObjectAdvanced>();
        resetableObjectAdvanced.onRecord += Record;
        resetableObjectAdvanced.onReset += Reset;
    }

    void OnDisable()
    {
        resetableObjectAdvanced.onRecord -= Record;
        resetableObjectAdvanced.onReset -= Reset;
    }


    public void Record()
    {
        startPos = Position;
        startRot = Rotation;
    }

    public void Reset()
    {
        Position = startPos;
        Rotation = startRot;
        rigidbody.ResetVelocity();
    }
    
    void Start ()
	{
	    rigidbody.velocity = Vector3.down * 20f;
	}

    private void OnCollisionEnter(Collision other)
    {
        var playerMoter = other.gameObject.GetComponentInParent<PlayerMoter>();
        if (playerMoter != null)
        {
            LastPlayerHit = playerMoter;
            var playerInstance = TeamManager.Instance.GetPlayerInstance(playerMoter);
            if( playerInstance != null )
                playerInstance.BallHit();
        }
    }
}
