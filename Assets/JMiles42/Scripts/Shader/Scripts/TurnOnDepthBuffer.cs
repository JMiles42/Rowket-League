using UnityEngine;

[ExecuteInEditMode]
public class TurnOnDepthBuffer: MonoBehaviour
{
	private void Start()
	{
		var camera = GetComponent<Camera>();

		if(camera)
			camera.depthTextureMode = DepthTextureMode.Depth;
	}
}