using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : JMilesBehaviour
{
    private Camera m_Camera;
    public new Camera camera
    {
        get
        {
            if (!m_Camera)
                m_Camera = GetComponent<Camera>();
            return m_Camera;
        }
        set { m_Camera = value; }
    }

    public static Transform overRidingLookAtTarget = null;
    public float smoothing;

    private void LateUpdate()
    {
        if(overRidingLookAtTarget)
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation,Quaternion.LookRotation(overRidingLookAtTarget.position),Time.deltaTime*smoothing);
    }
}
