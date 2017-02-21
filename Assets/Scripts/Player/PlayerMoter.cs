using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoter : JMilesRigidbodyBehaviour
{
    public PlayerMoterInputBase MyInput;

    private void OnEnable()
    {
        MyInput.Enable(this);
        MyInput.onLaunchPlayer += HitPuck;
    }

    private void OnDisable()
    {
        MyInput.onLaunchPlayer -= HitPuck;
        MyInput.Disable(this);
    }

    public void HitPuck()
    {
        var currentRotation = Quaternion.Euler(MyInput.GetMoveDirection());
        rigidbody.AddForce(transform.TransformDirection((currentRotation * transform.forward) * MyInput.GetMoveStrength()), ForceMode.Impulse);
    }
}
