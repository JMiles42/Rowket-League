using System;
using System.Collections;
using System.Collections.Generic;
using JMiles42.Data;
using UnityEngine;

public class PlayerNonGameplayMaster : Singleton<PlayerNonGameplayMaster>
{
    public bool GamePaused { get; private set; }
    public Action onPause;
    public Action onUnPause;

    private void OnEnable()
    {
        GamePaused = false;
    }

    private void OnDisable()
    {
    }

    private void Start()
    {
    }

    void Pause()
    {
        GamePaused = !GamePaused;

        if (GamePaused)
        {
            onPause.Trigger();
        }
        else
        {
            onUnPause.Trigger();
        }
    }
}
