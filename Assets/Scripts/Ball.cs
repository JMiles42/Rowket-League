using JMiles42.Data;
using UnityEngine;

[RequireComponent(typeof(ResetableObjectAdvanced))]
public class Ball: SingletonRigidbody<Ball>, IResetable
{
	private ResetableObjectAdvanced resetableObjectAdvanced;
	private Vector3                 startPos = Vector3.zero;
	private Quaternion              startRot;
	public  PlayerMotor             LastPlayerHit;

	private void OnEnable()
	{
		resetableObjectAdvanced          =  GetComponent<ResetableObjectAdvanced>();
		resetableObjectAdvanced.onRecord += Record;
		resetableObjectAdvanced.onReset  += Reset;
	}

	private void OnDisable()
	{
#if !UNITY_EDITOR
        resetableObjectAdvanced.onRecord -= Record;
        resetableObjectAdvanced.onReset -= Reset;
    #endif
	}

	private void Start()
	{
		rigidbody.velocity = Vector3.down * 20f;
	}

	private void OnCollisionEnter(Collision other)
	{
		var playerMotor = other.gameObject.GetComponentInParent<PlayerMotor>();

		if(playerMotor == null)
			return;

		LastPlayerHit = playerMotor;
		var playerInstance = TeamManager.Instance.GetPlayerInstance(playerMotor);

		if(playerInstance != null)
			playerInstance.BallHit();
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
}