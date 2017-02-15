using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoter : MonoBehaviour
{
    public PlayerMoterInputBase MyInput;

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
