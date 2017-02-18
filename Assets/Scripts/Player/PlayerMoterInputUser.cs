using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputUser", menuName = "Rowket/Input/User", order = 0)]
public class PlayerMoterInputUser : PlayerMoterInputBase
{
    public bool UserSubmit = false;
    public bool UserCoolingDown = false;
    public float RotationSpeed = 5;
    public Vector3 rotation;

    public float speed;
    public float strength = 0;
    public float strengthMultiplyer = 4;
    //private float maxStrength = 0;
    public float strengthPingPongMax = 10;

    public float RotDamp = 5;
    public float FollowDamp = 2;

    public float inputCooldown = 0.4f;

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

    public override float GetMoveStrength()
    {
        return strength * (strengthMultiplyer * (1 + strength));
    }

    public override void Enable(PlayerMoter callingObject)
    {
        Disable();
        PlayerInputManager.Instance.Horizontal.onKey += HorizontalPressed;
        PlayerInputManager.Instance.Jump.onKeyDown += JumpPressed;
        callingObject.StartRoutine(InputSession());
        callingObject.StartRoutine(PlayerUnique(callingObject));
    }

    public override void Disable(PlayerMoter callingObject)
    {
        Disable();
        callingObject.StopRoutine(InputSession());
        callingObject.StopRoutine(PlayerUnique(callingObject));
    }

    public void Disable()
    {
        PlayerInputManager.Instance.Horizontal.onKey -= HorizontalPressed;
        PlayerInputManager.Instance.Jump.onKeyDown -= JumpPressed;
    }

    private void JumpPressed()
    {
        if(UserCoolingDown) return;

        if(onLaunchPlayer != null) onLaunchPlayer();
        PlayerInputManager.Instance.StartCoroutine(InputDelay());
    }
    private IEnumerator InputDelay()
    {
        UserCoolingDown = true;
        yield return WaitForTimes.GetWaitForTime(inputCooldown);
        UserCoolingDown = false;
    }
    private IEnumerator InputSession()
    {
        while (true)
        {
            strength = Mathf.PingPong(Time.time, strengthPingPongMax);
            yield return null;
        }
    }

    private IEnumerator PlayerUnique(PlayerMoter callingObject)
    {
        bool update = true;
        Camera cam = callingObject.GetComponentInChildren<Camera>();
        if (cam == null)
        {
            var newCam = new InitWithComponent<Camera>("Player Camera");
            cam = newCam;
            newCam.gameObject.transform.parent = null;
        }
        cam.transform.parent = null;
        while (update)
        {
            //Get the direction of the camera from the input
            var currentRotation = Quaternion.Euler(GetMoveDirection());
            //Set the positon of the camera to "behind the players look direction"
            var newPosition = callingObject.transform.TransformPoint((currentRotation * -callingObject.transform.forward * 10) + Vector3.up * 4);
            //Get the camera looking at the player
            var newRotation = Quaternion.LookRotation(callingObject.transform.position - cam.transform.position);

            //Lerp the current values to the 
            cam.transform.position = Vector3.Lerp(cam.transform.position, newPosition, Time.deltaTime * FollowDamp);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, newRotation, Time.deltaTime * RotDamp);

            yield return null;
        }
        cam.transform.parent = callingObject.transform;
    }
}
