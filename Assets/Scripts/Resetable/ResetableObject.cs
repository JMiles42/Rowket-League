using UnityEngine;

/// <summary>
///     The most basic resetable oject class, only saves Rotation & Position
/// </summary>
public class ResetableObject: ResetableObjectBase
{
    /// <summary>
    ///     Transform to save/reset
    ///     if null it will reset the attached object
    /// </summary>
    public Transform transformToReset;
    /// <summary>
    ///     Saved Position
    /// </summary>
    public Vector3 Pos;
    /// <summary>
    ///     Saved Rotation
    /// </summary>
    public Quaternion Rot;

	private void Start()
	{
		if(transformToReset == null)
			transformToReset = transform;

		Record();
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		if(transformToReset == null)
			transformToReset = transform;

		Record();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		ResetableObjects.Remove(this);
	}

	[ContextMenu("Record Object")]
	public override void Record()
	{
		Pos = transformToReset.position;
		Rot = transformToReset.rotation;
	}

	[ContextMenu("Reset Object")]
	public override void Reset()
	{
		transformToReset.position = Pos;
		transformToReset.rotation = Rot;

		if(transformToReset.GetComponent<Rigidbody>())
			transformToReset.GetComponent<Rigidbody>().ResetVelocity();
	}
}