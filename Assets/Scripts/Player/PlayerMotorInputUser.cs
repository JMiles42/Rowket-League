using System.Collections;
using UnityEngine;

/// <summary>
/// This is the players input, it uses three differant Input Axis, to do the movement
/// TODO: Sepperate the camera from the Coroutine that all the movement is caculated
/// </summary>
[CreateAssetMenu(fileName = "PlayerOneInput", menuName = "Rowket/Input/User", order = 0)]
public class PlayerMotorInputUser : PlayerMotorInputBase
{
    //Set overriding camera look target for all instances of this class
    //Set by the Game master
    public static Transform lookAtTargetOverride;

    public static bool lookAtOverride = false;

    private bool userLaunch;
    private bool userJump;
    private bool userCoolingDown;
    private bool userCoolingDownJump;
    public float RotationSpeed = 5;

    private Vector3 rotation = Vector3.zero;

    public float speed;
    public float strength;
    public float strengthMin;
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
    private bool runBefore;

    private void HorizontalPressed()
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
        userCoolingDown = false;
        PlayerInputManager.GetAxisFromString(InputAxis).onKey += HorizontalPressed;
        PlayerInputManager.GetAxisFromString(InputLaunch).onKeyDown += LaunchPressed;
        PlayerInputManager.GetAxisFromString(InputJump).onKeyDown += JumpPressed;
        userLaunch = false;
        userJump = false;
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

    private void LaunchPressed()
    {
        if (userCoolingDown) return;
        userLaunch = true;
        PlayerInputManager.Instance.StartCoroutine(InputDelay());
    }

    private void JumpPressed()
    {
        if (userCoolingDownJump) return;
        userJump = true;
        PlayerInputManager.Instance.StartCoroutine(InputDelayJump());
    }

    private IEnumerator InputDelay()
    {
        userCoolingDown = true;
        yield return WaitForTimes.GetWaitForTime(inputCooldown);
        userCoolingDown = false;
    }

    private IEnumerator InputDelayJump()
    {
        userCoolingDownJump = true;
        yield return WaitForTimes.GetWaitForTime(inputCooldown);
        userCoolingDownJump = false;
    }

    private IEnumerator InputSession()
    {
        while (true)
        {
            strength = strengthMin + Mathf.PingPong(Time.time, strengthMax);
            yield return null;
        }
    }

    private IEnumerator PlayerUnique(PlayerMotor callingObject)
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
            arrow.gameObject.SetActive(!userCoolingDown);
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
            if (userJump)
            {
                callingObject.onLaunchPlayer.Trigger(Vector3.up * GetMoveStrength() / 3);
                userJump = false;
            }
            if (userLaunch)
            {
                callingObject.onLaunchPlayer.Trigger(GetMoveFinalDirection(callingObject.transform));
                userLaunch = false;
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