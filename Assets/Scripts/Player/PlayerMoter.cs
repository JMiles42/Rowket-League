using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoter : JMilesRigidbodyBehaviour
{
    public PlayerMoterInputBase MyInput;
    public float speed;

    void Update()
    {
        Vector3 input = MyInput.GetInput();
        rigidbody.AddRelativeForce(input * speed);
    }

    public void StartInput()
    {

    }

    IEnumerator InputSession()
    {
        while (!MyInput.GetInputSubmit())
        {
            yield return null;
        }
    }
}
