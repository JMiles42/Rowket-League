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

    public Action onGoal;

    public float TimerMax;

    void OnEnable()
    {
        onGoal += GameOver;
        onGameStart += EnableInput;
        onGameEnd += DisableInput;

        var goals = FindObjectsOfType<Goal>();
        foreach(var goal in goals) goal.onGoal += CallGoal;
    }
    void OnDisable()
    {
        onGoal -= GameOver;
        onGameStart -= EnableInput;
        onGameEnd -= DisableInput;

        var goals = FindObjectsOfType<Goal>();
        foreach (var goal in goals) goal.onGoal -= CallGoal;
    }
    void CallGoal()
    {
        if (onGoal != null) onGoal();
    }
    public void StartGame()
    {
        StartCoroutine(Countdown());
    }
    public void Start()
    {
        StartGame();
    }
    IEnumerator Countdown()
    {
        if (onGameStartCountdown != null) onGameStartCountdown();
        float timer = TimerMax;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            if (onGameCountdown != null) onGameCountdown(timer);
            yield return null;
        }
        if (onGameCountdown != null) onGameCountdown(0);
        if (onGameStart != null) onGameStart();
    }
    void EnableInput()
    {
        if (onGameInputEnable != null) onGameInputEnable();
    }
    void DisableInput()
    {
        if (onGameInputDisable != null) onGameInputDisable();
    }
    void GameOver()
    {
        StartCoroutine(GameEnd());
        if (onGameEnd != null) onGameEnd();
    }
    void Restart()
    {
        foreach(var reset in ResetableObject.ResetableObjects) reset.Reset();
        StartGame();
    }

    IEnumerator GameEnd()
    {
        float timer = TimerMax*2;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if (onGameCountdown != null) onGameCountdown(timer);
            yield return null;
        }
        if (onGameCountdown != null) onGameCountdown(0);

        Restart();
    }

}
