using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InputUser", menuName = "Rowket/Input/User", order = 0)]
public class PlayerMoterInputUser : PlayerMoterInputBase
{
    public bool UserSubmit = false;
    public bool UserCoolingDown;
    public float RotationSpeed = 5;
    public Vector3 rotation;

    public float speed;
    public float strength = 0;
    public float strengthMin = 0;
    public float strengthMultiplyer = 4;
    //private float maxStrength = 0;
    public float strengthMax = 2;

    public float RotDamp = 5;
    public float FollowDamp = 2;
    public float FollowDist = 5;

    public float inputCooldown = 0.4f;

    public Vector3 ScaleMin = new Vector3(0.1f,0.1f,0.1f);
    public Vector3 ScaleMax = new Vector3(0.5f,0.5f,1f);

    public GameObject ArrowPrefab;
    public GameObject CameraPrefab;

    public override Vector3 GetMoveDirection()
    {
        return rotation;
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
        UserCoolingDown = false;
        PlayerInputManager.Instance.Horizontal.onKey += HorizontalPressed;
        PlayerInputManager.Instance.Jump.onKeyDown += JumpPressed;
        callingObject.StartRoutine( InputSession() );
        callingObject.StartRoutine( PlayerUnique(callingObject) );
    }

    public override void Disable(PlayerMoter callingObject)
    {
        Disable();
        callingObject.StopRoutine( InputSession() );
        callingObject.StopRoutine( PlayerUnique(callingObject) );
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
        if (arrow == null)
        {
            GameObject newArrow = Instantiate(ArrowPrefab);
            arrow = newArrow.GetComponent<PlayerDisplayArrow>();
            newArrow.gameObject.transform.parent = callingObject.transform;
            newArrow.transform.ResetLocalPosRotScale();
        }
        arrow.gameObject.SetActive(true);

        cam.transform.parent = null;
        while (update)
        {
            arrow.gameObject.SetActive(!UserCoolingDown);
            arrow.SetScale((strength-strengthMin)/strengthMax);
            //Stores resualt to sav multiple calls
            var MoveDirection = GetMoveDirection();
            var currentRotation = Quaternion.Euler(MoveDirection);
            //Set the positon of the camera to "behind the players look direction"
            var newPosition = callingObject.transform.TransformPoint((currentRotation * -callingObject.transform.forward * 10) + callingObject.transform.up * 4) ;
            //Get the camera looking at the player
            var newRotation = Quaternion.LookRotation(callingObject.transform.position - (cam.transform.position + (Vector3.down * FollowDist)));
            var newPosativeRotation = callingObject.transform.TransformDirection((currentRotation * callingObject.transform.forward));

            //Lerp the current values to the 
            cam.transform.position = Vector3.Lerp(cam.transform.position, newPosition, Time.deltaTime * FollowDamp);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, newRotation, Time.deltaTime * RotDamp);

            arrow.transform.rotation = Quaternion.LookRotation(newPosativeRotation);

            yield return null;
        }
        cam.transform.parent = callingObject.transform;
    }
}
