using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoter : JMilesRigidbodyBehaviour
{
    public TeamType myTeam;
    public PlayerMoterInputBase MyInput;
    public string playerName = "";
    public static List<PlayerMoter> playerMoters = new List<PlayerMoter>();
    public Renderer meshRender;
    public List<Coroutine> ActiveCoroutines = new List<Coroutine>();

    public Action<Vector3> onLaunchPlayer;

    //private void OnEnable()
    public void OnSpawn()
    {
        MyInput.Init(this);
        playerMoters.Add(this);
        GameManager.Instance.onGameStart += StartInput;
        GameManager.Instance.onGameEnd += EndInput;
    }

    void OnDisable()
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
        rigidbody.AddForce(dir, ForceMode.Impulse);
    }

    public string GetName()
    {
        if(playerName == "")
            return playerName = MyInput.GetName();
        return playerName;
    }
    /// <summary>
    /// Gets the closest player moter to the pos passed to
    /// </summary>
    /// <param name="pos">Position to get closest moter to</param>
    /// <returns>Player moter closest to pos</returns>
    public static PlayerMoter GetClosestMoter(Vector3 pos)
    {
        var closest = playerMoters[0];
        float dist = Vector3.Distance(pos, closest.Position);
        for (int i = 0, j = playerMoters.Count; i < j; i++)
        {
            var other = playerMoters[i];
            float dist1 = Vector3.Distance(pos, other.Position);
            if (!(dist1 < dist)) continue;
            closest = other;
            dist = dist1;
        }
        return closest;
    }

    /// <summary>
    /// Gets the closest player moter to the pos passed to
    /// </summary>
    /// <param name="pos">Position to get closest moter to</param>
    /// <param name="callingObject">Pass along caller, to make it not find its self</param>
    /// <returns>Player moter closest to pos</returns>
    public static PlayerMoter GetClosestMoter(Vector3 pos, PlayerMoter callingObject)
    {
        if (playerMoters.Count == 0)
            return callingObject;

        var closest = playerMoters[0];
        float dist = Vector3.Distance(pos, closest.Position);
        for (int i = 0, j = playerMoters.Count; i < j; i++)
        {
            var other = playerMoters[i];
            if (callingObject == other)
            {
                if (i < j)
                    continue;
            }

            float dist1 = Vector3.Distance(pos, other.Position);
            if (!(dist1 < dist)) continue;
            closest = other;
            dist = dist1;
        }
        return closest;
    }


    /// <summary>
    /// Gets the closest player moter to the pos passed to
    /// </summary>
    /// <param name="pos">Position to get closest moter to</param>
    /// <param name="callingObject">Pass along caller, to make it not find its self</param>
    /// <param name="team">Team to ignore</param>
    /// <returns>Player moter closest to pos</returns>
    public static PlayerMoter GetClosestMoter(Vector3 pos, PlayerMoter callingObject, TeamType team)
    {
        if (playerMoters.Count == 0)
            return callingObject;

        var closest = playerMoters[0];
        float dist = Vector3.Distance(pos, closest.Position);
        for (int i = 0, j = playerMoters.Count; i < j; i++)
        {
            var other = playerMoters[i];
            if (callingObject == other || callingObject.myTeam == team)
            {
                if (i < j)
                    continue;
            }

            float dist1 = Vector3.Distance(pos, other.Position);
            if (!(dist1 < dist)) continue;
            closest = other;
            dist = dist1;
        }
        return closest;
    }

    //TODO:move to another component
    public void SetTeam(TeamType myNewTeam)
    {
        if (myNewTeam == TeamType.Blue)
        {
            myTeam = myNewTeam;
            meshRender.materials = new[] {meshRender.material, TeamManager.Instance.BlueTeam.mat};
        }
        else
        {
            myTeam = myNewTeam;
            meshRender.materials = new[] {meshRender.material, TeamManager.Instance.RedTeam.mat};
        }
    }

    public void SetInput(PlayerMoterInputBase input)
    {
        MyInput = input;
        SetGameobjectName();
    }

    public void SetName(string str)
    {
        playerName = str;
    }

    private void OnValidate()
    {
        SetGameobjectName();
    }

    private void SetGameobjectName()
    {
        gameObject.name = myTeam.ToString()[0] + " : " + GetName() + " : " + MyInput;
    }
}
