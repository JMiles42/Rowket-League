using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputUser", menuName = "Rowket/Input/User", order = 0)]
public class PlayerMoterInputUser : PlayerMoterInputBase
{
    public bool UserSubmit = false;
    public float RotationSpeed = 5;
    public Vector3 rotation;

    public override Vector3 GetMoveDirection()
    {
        return rotation;
    }

    public override Vector2 GetInput()
    {
        return new Vector2(PlayerInputManager.Instance.Horizontal,PlayerInputManager.Instance.Vertical);
    }

    public override bool GetInputSubmit()
    {
        return UserSubmit;
    }

    private void HorizontalPressed()
    {
        rotation += Vector3.up * PlayerInputManager.Instance.Horizontal;
    }

    private void OnEnable()
    {
        StaticUnityEventManager.StartListening(PlayerInputValues.Horizontal, HorizontalPressed);
    }

    private void OnDisable()
    {
        StaticUnityEventManager.StopListening(PlayerInputValues.Horizontal, HorizontalPressed);
    }
}
