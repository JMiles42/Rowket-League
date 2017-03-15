using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : JMilesBehaviour
{
    public void Start()
    {
        GetComponent<Camera>().pixelRect = SplitScreenCameraUtilities.SetCameraRect(SplitScreenCameraUtilities.CameraMode.ThreePlayerLowerMiddle);
    }
    private void LateUpdate()
    {
        
    }
}
