using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoter : JMilesRigidbodyBehaviour
{
    public TeamType myTeam;
    public PlayerMoterInputBase MyInput;
    public static List<PlayerMoter> playerMoters = new List<PlayerMoter>();
    public List<Coroutine> ActiveCoroutines = new List<Coroutine>();

    public Action<Vector3> onLaunchPlayer;

    private void OnEnable()
    {
        MyInput.Init(this);
        playerMoters.Add(this);
        GameManager.Instance.onGameStart += StartInput;
        GameManager.Instance.onGameEnd += EndInput;
    }

    private void OnDisable()
    {
        playerMoters.Remove(this);
        GameManager.Instance.onGameStart -= StartInput;
        GameManager.Instance.onGameEnd -= EndInput;
        EndInput();
    }

    public void StartInput()
    {
        MyInput.Enable(this);
        onLaunchPlayer += HitPuck;
    }

    public void EndInput()
    {
        onLaunchPlayer -= HitPuck;
        MyInput.Disable(this);
    }

    public void HitPuck(Vector3 dir)
    {
        Debug.Log("Hit in :" + dir);
        rigidbody.AddForce(dir, ForceMode.Impulse);
        //var currentRotation = Quaternion.Euler(MyInput.GetMoveDirection());
        //rigidbody.AddForce(transform.TransformDirection((currentRotation * transform.forward) * MyInput.GetMoveStrength()), ForceMode.Impulse);
    }

    public static PlayerMoter GetClosestMoter(Vector3 pos)
    {
        PlayerMoter closest = playerMoters[0];
        var dist = Vector3.Distance(pos, closest.Position);
        for (int i = 0, j = playerMoters.Count; i < j; i++)
        {
            var other = playerMoters[i];
            var dist1 = Vector3.Distance(pos, other.Position);
            if (dist1 < dist)
            {
                closest = other;
                dist = dist1;
            }
        }
        return closest;
    }
    public static PlayerMoter GetClosestMoter(Vector3 pos, PlayerMoter callingObject)
    {
        if (playerMoters.Count == 0)
            return callingObject;

        PlayerMoter closest = playerMoters[0];
        var dist = Vector3.Distance(pos, closest.Position);
        for (int i = 0, j = playerMoters.Count; i < j; i++)
        {
            var other = playerMoters[i];
            if (callingObject == other)
            {
                if (i < j)
                    continue;
                break;
            }

            var dist1 = Vector3.Distance(pos, other.Position);
            if (dist1 < dist)
            {
                closest = other;
                dist = dist1;
            }
        }
        return closest;
    }

    public static PlayerMoter GetClosestMoter(Vector3 pos, PlayerMoter callingObject,TeamType team)
    {
        if (playerMoters.Count == 0)
            return callingObject;

        PlayerMoter closest = playerMoters[0];
        var dist = Vector3.Distance(pos, closest.Position);
        for (int i = 0, j = playerMoters.Count; i < j; i++)
        {
            var other = playerMoters[i];
            if (callingObject == other || callingObject.myTeam == team)
            {
                if (i < j)
                    continue;
                else break;
            }

            var dist1 = Vector3.Distance(pos, other.Position);
            if (dist1 < dist)
            {
                closest = other;
                dist = dist1;
            }
        }
        return closest;
    }

    private void OnValidate()
    {
        gameObject.name = myTeam.ToString() + " : " + MyInput.ToString();
    }
}
