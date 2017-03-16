using UnityEngine;

public class ResetableObject : ResetableObjectBase
{
    public Transform transformToReset;

    public Vector3 Pos;
    public Quaternion Rot;

    void Start()
    {
        if (transformToReset == null) transformToReset = transform;
        Record();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (transformToReset == null) transformToReset = transform;
        Record();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ResetableObjects.Remove(this);
    }

    public override void Record()
    {
        Pos = transformToReset.position;
        Rot = transformToReset.rotation;
    }

    public override void Reset()
    {
        transformToReset.position = Pos;
        transformToReset.rotation = Rot;
        if (transformToReset.GetComponent<Rigidbody>())
            transformToReset.GetComponent<Rigidbody>().ResetVelocity();
    }
}
