using UnityEngine;

/// <summary>
///     Makes a camera follow a ball, when there is a ball in the scene
/// </summary>
public class MainCamFollow: JMilesBehaviour
{
	public Transform target;

	private void LateUpdate()
	{
		if(!target)
		{
			if(FindObjectOfType<Ball>())
				target = FindObjectOfType<Ball>().transform;
		}

		if(target)
			transform.LookAt(target);
	}
}