using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerOneInput", menuName = "Rowket/Input/User", order = 0)]
public class PlayerMotorInputUser : PlayerMotorInputBase
{
    //Set overriding camera look target for all instances of this class
    //Set by the Game master
    public static Transform lookAtTargetOverride;
    public static bool lookAtOverride = false;

    bool UserLaunch = false;
    bool UserJump = false;
    bool UserCoolingDown = false;
    bool UserCoolingDownJump = false;
    public float RotationSpeed = 5;

    Vector3 rotation = Vector3.zero;

    public float speed;
    public float strength = 0;
    public float strengthMin = 0;
    public float strengthMultiplyer = 4;
    public float strengthMax = 2;

    public float RotDamp = 5;
    public float FollowDamp = 2;
    public float FollowDist = 5;

    //Strings for the three differant input axis to be listening to
    public string InputAxis;
    public string InputLaunch;
    public string InputJump;

    public float inputCooldown = 0.4f;

    public Vector3 ScaleMin = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 ScaleMax = new Vector3(0.5f, 0.5f, 1f);

    public GameObject ArrowPrefab;
    public GameObject CameraPrefab;
    bool runBefore = false;

    void HorizontalPressed()
    {
        rotation += Vector3.up * PlayerInputManager.Instance.Horizontal;
    }

    public override float GetMoveStrength()
    {
        return strength * (strengthMultiplyer * (1 + strength));
    }

    public override void Enable(PlayerMotor callingObject)
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

    public override void Disable(PlayerMotor callingObject)
    {
        Disable();
    }

    public void Disable()
    {
        PlayerInputManager.GetAxisFromString(InputAxis).onKey -= HorizontalPressed;
        PlayerInputManager.GetAxisFromString(InputLaunch).onKeyDown -= LaunchPressed;
        PlayerInputManager.GetAxisFromString(InputJump).onKeyDown -= JumpPressed;
    }

    public override void Init(PlayerMotor callingObject)
    {
        callingObject.ActiveCoroutines.Add(callingObject.StartRoutine(PlayerUnique(callingObject)));
    }

    void LaunchPressed()
    {
        if (UserCoolingDown) return;
        UserLaunch = true;
        PlayerInputManager.Instance.StartCoroutine(InputDelay());
    }

    void JumpPressed()
    {
        if (UserCoolingDownJump) return;
        UserJump = true;
        PlayerInputManager.Instance.StartCoroutine(InputDelayJump());
    }

    IEnumerator InputDelay()
    {
        UserCoolingDown = true;
        yield return WaitForTimes.GetWaitForTime(inputCooldown);
        UserCoolingDown = false;
    }

    IEnumerator InputDelayJump()
    {
        UserCoolingDownJump = true;
        yield return WaitForTimes.GetWaitForTime(inputCooldown);
        UserCoolingDownJump = false;
    }

    IEnumerator InputSession()
    {
        while (true)
        {
            strength = strengthMin + Mathf.PingPong(Time.time, strengthMax);
            yield return null;
        }
    }

    IEnumerator PlayerUnique(PlayerMotor callingObject)
    {
        var cam = callingObject.GetComponentInChildren<Camera>();
        var arrow = callingObject.GetComponentInChildren<PlayerDisplayArrow>();
        if (cam == null)
        {
            var newCam = Instantiate(CameraPrefab);
            cam = newCam.GetComponent<Camera>();
            newCam.gameObject.transform.parent = null;
        }
        cam.gameObject.SetActive(true);

        if (arrow == null)
        {
            var newArrow = Instantiate(ArrowPrefab);
            arrow = newArrow.GetComponent<PlayerDisplayArrow>();
            newArrow.gameObject.transform.parent = callingObject.transform;
            newArrow.transform.ResetLocalPosRotScale();
        }
        arrow.gameObject.SetActive(true);

        cam.transform.parent = null;

        while (true)
        {
            arrow.gameObject.SetActive(!UserCoolingDown);
            arrow.SetScale((strength - strengthMin) / strengthMax);
            //Stores result to sav multiple calls
            var MoveDirection = rotation;
            var currentRotation = Quaternion.Euler(MoveDirection);
            //Set the positon of the camera to "behind the players look direction"
            var newPosition = callingObject.transform.TransformPoint(
                (currentRotation * -callingObject.transform.forward * 10) + callingObject.transform.up * 4);
            //Get the camera looking at the player
            var newRotation = Quaternion.LookRotation(callingObject.transform.position -
                                                      (cam.transform.position + (Vector3.down * FollowDist)));
            var newPositiveRotation =
                callingObject.transform.TransformDirection((currentRotation * callingObject.transform.forward));

            //Lerp the current values to the 
            cam.transform.position = Vector3.Lerp(cam.transform.position, newPosition, Time.deltaTime * FollowDamp);
            if (lookAtOverride)
                cam.transform.LookAt(lookAtTargetOverride);
            else
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, newRotation, Time.deltaTime * RotDamp);

            arrow.transform.rotation = Quaternion.LookRotation(newPositiveRotation);
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