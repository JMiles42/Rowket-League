using UnityEngine;

public static class UnityClassExtensions
{
	public static void ResetLocalPosRotScale(this Transform transform)
	{
		transform.localPosition    = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;
		transform.localScale       = Vector3.one;
	}

	public static void ResetPosRotScale(this Transform transform)
	{
		transform.position    = Vector3.zero;
		transform.eulerAngles = Vector3.zero;
		transform.localScale  = Vector3.one;
	}

	public static void ResetVelocity(this Rigidbody rigidbody)
	{
		rigidbody.velocity        = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}
}