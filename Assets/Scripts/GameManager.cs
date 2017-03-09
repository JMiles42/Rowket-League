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

    public StringListScriptableObject AiNames;

    public SpawnLayout[] SpawnLayouts;

    public PlayerMoterInputAI[] AiInputSystems;
    public PlayerMoterInputUser InputUser;


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
        onGoal.Trigger();
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
        onGameStartCountdown.Trigger();
        float timer = TimerMax;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            onGameCountdown.Trigger(timer);
            yield return null;
        }
        onGameCountdown.Trigger(0);
        onGameStart.Trigger();
    }
    void EnableInput()
    {
        onGameInputEnable.Trigger();
    }
    void DisableInput()
    {
        onGameInputDisable.Trigger();
    }
    void GameOver()
    {
        StartCoroutine(GameEnd());
        onGameEnd.Trigger();
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
            onGameCountdown.Trigger(timer);
            yield return null;
        }
        onGameCountdown.Trigger(0);

        Restart();
    }

}
