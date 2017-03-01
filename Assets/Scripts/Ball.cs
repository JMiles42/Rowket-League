using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : JMilesRigidbodyBehaviour,IResetable
{
    private Vector3 startpos = Vector3.zero;
    private Quaternion startRot = new Quaternion();
    public PlayerMoter LastPlayerHit;
    
    public void Record()
    {
        startpos = Position;
    }

    public void Reset()
    {
        Position = startpos;
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
