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

    private void JumpPressed()
    {
        UserSubmit = true;
    }

    private void JumpReleased()
    {
        UserSubmit = false;
    }

    private void OnEnable()
    {
        PlayerInputManager.Instance.Horizontal.onKey += HorizontalPressed;
        PlayerInputManager.Instance.Jump.onKeyDown += JumpPressed;
        PlayerInputManager.Instance.Jump.onKeyUp += JumpReleased;
    }

    private void OnDisable()
    {
        PlayerInputManager.Instance.Horizontal.onKey -= HorizontalPressed;
        PlayerInputManager.Instance.Jump.onKeyDown -= JumpPressed;
        PlayerInputManager.Instance.Jump.onKeyUp -= JumpReleased;
    }
}
