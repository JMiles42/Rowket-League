﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    void OnEnable()
    {
        RecordLevel();
        TeamManager.Instance.onGoal += ResetLevel;
    }
    void OnDisable()
    {
        TeamManager.Instance.onGoal -= ResetLevel;
    }
    void Start()
    {
        RecordLevel();
    }
    public void RecordLevel()
    {
        var resetable = JMiles42.InterfaceHelper.FindObjects<IResetable>();
        for (int i = 0, j = resetable.Count; i < j; i++)
            resetable[i].Record();
    }
    public void ResetLevel()
    {
        var resetable = JMiles42.InterfaceHelper.FindObjects<IResetable>();
        for (int i = 0, j = resetable.Count; i < j; i++)
            resetable[i].Reset();
    }
}
