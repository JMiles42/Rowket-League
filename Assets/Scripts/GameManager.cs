using JMiles42.Data;
using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Action<float> onGameCountdown;
    public Action onGameStartCountdown;
    public Action onGameStart;
    public Action onGameEnd;
    public Action onGameInputEnable;
    public Action onGameInputDisable;

    public float TimerMax;

    void OnEnable()
    {
        onGameStart += EnableInput;
        onGameEnd += DisableInput;
    }
    void OnDisable()
    {
        onGameStart -= EnableInput;
        onGameEnd -= DisableInput;
    }
    void Start()
    {
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        if (onGameStartCountdown != null)
            onGameStartCountdown();
        float timer = TimerMax;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            if (onGameCountdown != null)
                onGameCountdown(timer);
            yield return null;
        }
        if (onGameCountdown != null)
            onGameCountdown(0);
        if (onGameStart != null)
            onGameStart();
    }
    void EnableInput()
    {
        if (onGameInputEnable != null)
            onGameInputEnable();
    }
    void DisableInput()
    {
        if (onGameInputDisable != null)
            onGameInputDisable();
    }
}
