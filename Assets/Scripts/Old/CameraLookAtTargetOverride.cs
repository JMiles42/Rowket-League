using UnityEngine;

public class CameraLookAtTargetOverride: JMilesBehaviour
{
	public static Transform overRidingLookAtTarget = null;
	public static bool      lookAtTarget           = false;
	private       Camera    m_Camera;
	public        float     smoothing;
	public new Camera camera
	{
		get
		{
			if(!m_Camera)
				m_Camera = GetComponent<Camera>();

			return m_Camera;
		}
		set { m_Camera = value; }
	}

	private void LateUpdate()
	{
		if(lookAtTarget)
			camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, Quaternion.LookRotation(overRidingLookAtTarget.position), Time.deltaTime * smoothing);
	}
}