using UnityEngine;
using JMiles42.Data;

[RequireComponent(typeof(ResetableObjectAdvanced))]
public class Ball : SingletonRigidbody<Ball>, IResetable
{
    ResetableObjectAdvanced resetableObjectAdvanced;
    Vector3 startPos = Vector3.zero;
    Quaternion startRot;
    public PlayerMotor LastPlayerHit;


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

    void OnCollisionEnter(Collision other)
    {
        var playerMotor = other.gameObject.GetComponentInParent<PlayerMotor>();
        if (playerMotor == null) return;
        LastPlayerHit = playerMotor;
        var playerInstance = TeamManager.Instance.GetPlayerInstance(playerMotor);
        if( playerInstance != null )
            playerInstance.BallHit();
    }
}
