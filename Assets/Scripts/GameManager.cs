using JMiles42.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ran = UnityEngine.Random;
using GameSettings = GameSettingsManagerMaster;

public class GameManager : Singleton<GameManager>
{
    public Action<float> onGameCountdown;
    public Action onGameStartCountdown;
    public Action onGameStart;
    public Action onGameEnd;
    public Action onGameInputEnable;
    public Action onGameInputDisable;

    public Action onAnyGoal;

    public StringListScriptableObject AiNames;

    public SpawnLayout[] SpawnLayouts;

    public PlayerMoterInputAI[] AiInputSystems;
    public PlayerMoterInputUser PlayerOneInput;
    public PlayerMoterInputUser PlayerTwoInput;
    public PlayerMoterInputUser PlayerThreeInput;
    public PlayerMoterInputUser PlayerFourInput;

    public PlayerMoter prefabMoter;
    public Ball prefabBall;

    public ButtonClickEvent startGameBtn;

    public Transform PlayersFolder;
    public float TimerMax;

    private void OnEnable()
    {
        onAnyGoal += GameOver;
        onGameStart += EnableInput;
        onGameEnd += DisableInput;
        if (startGameBtn)
            startGameBtn.onMouseClick += StartGame;

        var goals = FindObjectsOfType<Goal>();
        foreach (var goal in goals) goal.onGoal += CallGoal;
    }

    private void OnDisable()
    {
        onAnyGoal -= GameOver;
        onGameStart -= EnableInput;
        onGameEnd -= DisableInput;

        if (startGameBtn)
            startGameBtn.onMouseClick -= StartGame;

        var goals = FindObjectsOfType<Goal>();
        foreach (var goal in goals) goal.onGoal -= CallGoal;
    }

    private void CallGoal()
    {
        onAnyGoal.Trigger();
    }

    public void StartGame()
    {
        GameSettings.Instance.BuildArrays();
        SpawnBall();
        SpawnPlayers();
        StartCoroutine(Countdown());
    }

    public void RestartGame()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        onGameStartCountdown.Trigger();
        float timer = TimerMax;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            onGameCountdown.Trigger(timer);
            yield return null;
        }
        onGameCountdown.Trigger(0);
        onGameStart.Trigger();
    }

    private void EnableInput()
    {
        onGameInputEnable.Trigger();
    }

    private void DisableInput()
    {
        onGameInputDisable.Trigger();
    }

    private void GameOver()
    {
        StartCoroutine(GameEnd());
        onGameEnd.Trigger();
    }

    private void Restart()
    {
        foreach (var reset in ResetableObjectBase.ResetableObjects) reset.Reset();
        RestartGame();
    }

    private IEnumerator GameEnd()
    {
        yield return WaitForTimes.GetWaitForTime(3);
        Restart();
    }

    public PlayerMoterInputBase GetInputClass(AiAgressiveMode mode, AiReactionTime time)
    {
        switch (mode)
        {
            case AiAgressiveMode.PlayerOne:
                return PlayerOneInput;
            case AiAgressiveMode.PlayerTwo:
                return PlayerTwoInput;
            case AiAgressiveMode.PlayerThree:
                return PlayerThreeInput;
            case AiAgressiveMode.PlayerFour:
                return PlayerFourInput;
        }

        for (int i = 0, j = AiInputSystems.Length; i < j; i++)
            if (AiInputSystems[i].AiMode == mode && AiInputSystems[i].ReactionTime == time)
                return AiInputSystems[i];

        return AiInputSystems[Ran.Range(0, AiInputSystems.Length)];
    }

    private void SpawnPlayers()
    {
        var redTeamAmount = GameSettings.Instance.RedTeamComposition.Length;
        var blueTeamAmount = GameSettings.Instance.BlueTeamComposition.Length;

        var biggestTeam = Mathf.Max(redTeamAmount, blueTeamAmount);

        SpawnLayout layout = null;

        {
            var layouts = SpawnLayouts.Where(spawnLayout => spawnLayout.Positions.Length >= biggestTeam).ToList();
            layout = layouts[Ran.Range(0, layouts.Count)];
        }

        for (int i = 0, j = biggestTeam; i < j; i++)
        {
            if (redTeamAmount > i)
                SpawnPlayer(GameSettings.Instance.RedTeamComposition[i], layout.Positions[i],
                    TeamManager.Instance.RedTeam,
                    GameSettings.Instance.RedTeam[GameSettings.Instance.RedTeamComposition[i]].Name);
            if (blueTeamAmount > i)
                SpawnPlayer(GameSettings.Instance.BlueTeamComposition[i], -layout.Positions[i],
                    TeamManager.Instance.BlueTeam,
                    GameSettings.Instance.BlueTeam[GameSettings.Instance.BlueTeamComposition[i]].Name);
        }
    }

    private void SpawnBall()
    {
        Instantiate(prefabBall.gameObject, new Vector3(0, 30, 0), Quaternion.identity);
    }

    private void SpawnPlayer(PlayerMoterInputBase player, Vector3 pos, TeamManager.TeamInstance team, string name = "")
    {
        var newPlayer = Instantiate(prefabMoter.gameObject, pos, Quaternion.identity, PlayersFolder);
        var newPlayerMoter = newPlayer.GetComponent<PlayerMoter>();
        newPlayerMoter.SetInput(player);
        newPlayerMoter.SetTeam(team.team.myTeam);
        newPlayerMoter.SetName(name);
        newPlayerMoter.OnSpawn();
        TeamManager.Instance.players.Add(new TeamManager.PlayerInstance(newPlayerMoter));
    }


/*
    public SpawnLayout layout;

    private void OnDrawGizmos()
    {
        if (layout)
            if(layout.Positions.Length > 0)
                foreach (Vector3 v in layout.Positions)
                    Gizmos.DrawSphere(v,1);
    }
*/
}