using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TurnOnDepthBuffer : MonoBehaviour
{

    void Start()
    {
        var camera = GetComponent<Camera>();
        if (camera)
            camera.depthTextureMode = DepthTextureMode.Depth;
    }
}
