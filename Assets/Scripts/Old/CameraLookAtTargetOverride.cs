using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAtTargetOverride : JMilesBehaviour
{
    Camera m_Camera;
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
    public static bool lookAtTarget = false;
    public float smoothing;

    void LateUpdate()
    {
        if(lookAtTarget)
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation,Quaternion.LookRotation(overRidingLookAtTarget.position),Time.deltaTime*smoothing);
    }
}
