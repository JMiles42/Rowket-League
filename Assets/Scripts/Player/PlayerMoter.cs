using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoter : JMilesRigidbodyBehaviour
{
    public PlayerMoterInputBase MyInput;
    public bool IsPlayer;
    public float speed;
    public float strength = 0;
    public float strengthMultiplyer = 4;
    //private float maxStrength = 0;
    public float strengthPingPongMax = 10;
    void Start()
    {
        {
            var playerInput = MyInput as PlayerMoterInputUser;
            if (playerInput != null)
            {
                IsPlayer = true;
                StartCoroutine(PlayerUnique());
            }
        }
        //maxStrength = strength;
        StartInput();
    }

    public void StartInput()
    {
        StartCoroutine(InputSession());
    }

    IEnumerator InputSession()
    {
        while(true)
        {
            while (!MyInput.GetInputSubmit())
            {
                //yCenter + Mathf.PingPong(Time.time * 2, strengthPingPongMax) - strengthPingPongMax/2f
                strength = Mathf.PingPong(Time.time, strengthPingPongMax);
                yield return null;
            }

            var currentRotation = Quaternion.Euler (MyInput.GetMoveDirection());
            rigidbody.AddForce((currentRotation * transform.forward)*strength*strengthMultiplyer,ForceMode.Impulse);
        }
    }

    IEnumerator PlayerUnique()
    {
        bool update = true;
        Camera cam = GetComponentInChildren<Camera>();
        while (update)
        {
            var currentRotation = Quaternion.Euler (MyInput.GetMoveDirection());

            cam.transform.position = (currentRotation * -transform.forward * 10)+transform.up*4;
            cam.transform.rotation = Quaternion.LookRotation(transform.position-cam.transform.position);
            yield return null;
        }
    }
}
