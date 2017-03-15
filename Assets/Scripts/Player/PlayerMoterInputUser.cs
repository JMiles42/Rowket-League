using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PlayerOneInput", menuName = "Rowket/Input/User", order = 0)]
public class PlayerMoterInputUser : PlayerMoterInputBase
{
    bool UserLaunch = false;
    bool UserJump = false;
    bool UserCoolingDown = false;
    bool UserCoolingDownJump = false;
    public float RotationSpeed = 5;

    private Vector3 rotation = Vector3.zero;

    public float speed;
    public float strength = 0;
    public float strengthMin = 0;
    public float strengthMultiplyer = 4;
    public float strengthMax = 2;

    public float RotDamp = 5;
    public float FollowDamp = 2;
    public float FollowDist = 5;

    public string InputAxis;
    public string InputLaunch;
    public string InputJump;

    public float inputCooldown = 0.4f;

    public Vector3 ScaleMin = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 ScaleMax = new Vector3(0.5f, 0.5f, 1f);

    public GameObject ArrowPrefab;
    public GameObject CameraPrefab;
    private bool runBefore = false;

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
        UserCoolingDown = false;
        PlayerInputManager.GetAxisFromString(InputAxis).onKey += HorizontalPressed;
        PlayerInputManager.GetAxisFromString(InputLaunch).onKeyDown += LaunchPressed;
        PlayerInputManager.GetAxisFromString(InputJump).onKeyDown += JumpPressed;
        UserLaunch = false;
        UserJump = false;
        if (runBefore) return;
        callingObject.ActiveCoroutines.Add(callingObject.StartRoutine(InputSession()));
        runBefore = true;
    }

    public override void Disable(PlayerMoter callingObject)
    {
        Disable();
    }

    public void Disable()
    {
        PlayerInputManager.GetAxisFromString(InputAxis).onKey -= HorizontalPressed;
        PlayerInputManager.GetAxisFromString(InputLaunch).onKeyDown -= LaunchPressed;
        PlayerInputManager.GetAxisFromString(InputJump).onKeyDown -= JumpPressed;
    }

    public override void Init(PlayerMoter callingObject)
    {
        callingObject.ActiveCoroutines.Add(callingObject.StartRoutine(PlayerUnique(callingObject)));
    }

    private void LaunchPressed()
    {
        if (UserCoolingDown) return;
        UserLaunch = true;
        PlayerInputManager.Instance.StartCoroutine(InputDelay());
    }

    private void JumpPressed()
    {
        if (UserCoolingDownJump) return;
        UserJump = true;
        PlayerInputManager.Instance.StartCoroutine(InputDelayJump());
    }

    private IEnumerator InputDelay()
    {
        UserCoolingDown = true;
        yield return WaitForTimes.GetWaitForTime(inputCooldown);
        UserCoolingDown = false;
    }

    private IEnumerator InputDelayJump()
    {
        UserCoolingDownJump = true;
        yield return WaitForTimes.GetWaitForTime(inputCooldown);
        UserCoolingDownJump = false;
    }

    private IEnumerator InputSession()
    {
        while (true)
        {
            strength = strengthMin + Mathf.PingPong(Time.time, strengthMax);
            yield return null;
        }
    }

    private IEnumerator PlayerUnique(PlayerMoter callingObject)
    {
        bool update = true;
        Camera cam = callingObject.GetComponentInChildren<Camera>();

        PlayerDisplayArrow arrow = callingObject.GetComponentInChildren<PlayerDisplayArrow>();
        if (cam == null)
        {
            GameObject newCam = Instantiate(CameraPrefab);
            cam = newCam.GetComponent<Camera>();
            newCam.gameObject.transform.parent = null;
        }
        cam.gameObject.SetActive(true);
        //cam.tag = "MainCamera";

        if (arrow == null)
        {
            var newArrow = Instantiate(ArrowPrefab);
            arrow = newArrow.GetComponent<PlayerDisplayArrow>();
            newArrow.gameObject.transform.parent = callingObject.transform;
            newArrow.transform.ResetLocalPosRotScale();
        }
        arrow.gameObject.SetActive(true);

        cam.transform.parent = null;
        while (update)
        {
            arrow.gameObject.SetActive(!UserCoolingDown);
            arrow.SetScale((strength - strengthMin) / strengthMax);
            //Stores resualt to sav multiple calls
            var MoveDirection = rotation;
            var currentRotation = Quaternion.Euler(MoveDirection);
            //Set the positon of the camera to "behind the players look direction"
            var newPosition = callingObject.transform.TransformPoint(
                (currentRotation * -callingObject.transform.forward * 10) + callingObject.transform.up * 4);
            //Get the camera looking at the player
            var newRotation = Quaternion.LookRotation(callingObject.transform.position -
                                                      (cam.transform.position + (Vector3.down * FollowDist)));
            var newPosativeRotation =
                callingObject.transform.TransformDirection((currentRotation * callingObject.transform.forward));

            //Lerp the current values to the 
            cam.transform.position = Vector3.Lerp(cam.transform.position, newPosition, Time.deltaTime * FollowDamp);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, newRotation, Time.deltaTime * RotDamp);

            arrow.transform.rotation = Quaternion.LookRotation(newPosativeRotation);
            if (UserJump)
            {
                callingObject.onLaunchPlayer.Trigger(Vector3.up * GetMoveStrength() / 3);
                UserJump = false;
            }
            if (UserLaunch)
            {
                callingObject.onLaunchPlayer.Trigger(GetMoveFinalDirection(callingObject.transform));
                UserLaunch = false;
            }
            yield return null;
        }
        cam.transform.parent = callingObject.transform;
    }

    public Vector3 GetMoveFinalDirection(Transform t)
    {
        return t.TransformDirection(Quaternion.Euler(rotation) * t.forward * GetMoveStrength());
    }

    public override string GetName()
    {
        return "Player";
    }
}