using JMiles42.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ran = UnityEngine.Random;
using GameSettings = GameSettingsManagerMaster;
using CamUtils = SplitScreenCamUtils;

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
    public Transform lookAtScoreBoard;

    void OnEnable()
    {
        onAnyGoal += GameOver;

        PlayerMoterInputUser.lookAtTargetOverride = lookAtScoreBoard;
        onAnyGoal += delegate { PlayerMoterInputUser.lookAtOverride = true; };
        onGameStart += delegate { PlayerMoterInputUser.lookAtOverride = false; };

        onGameStart += EnableInput;
        onGameEnd += DisableInput;
        startGameBtn.onMouseClick += StartGame;

        var goals = FindObjectsOfType<Goal>();
        foreach (var goal in goals)
            goal.onGoal += CallAnyGoal;
    }

    void OnDisable()
    {
        onAnyGoal -= GameOver;
        onGameStart -= EnableInput;
        onGameEnd -= DisableInput;

        startGameBtn.onMouseClick -= StartGame;

        var goals = FindObjectsOfType<Goal>();
        foreach (var goal in goals)
            goal.onGoal -= CallAnyGoal;
    }

    void CallAnyGoal()
    {
        onAnyGoal.Trigger();
    }

    public void StartGame()
    {
        GameSettings.Instance.BuildArrays();
        SpawnBall();
        SpawnPlayers();
        SetUpCamera();
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

    public PlayerMoterInputBase GetInputClass(AiAggressiveMode mode, AiReactionTime time)
    {
        //Check if it is a player
        switch (mode)
        {
            case AiAggressiveMode.PlayerOne:
                return PlayerOneInput;
            case AiAggressiveMode.PlayerTwo:
                return PlayerTwoInput;
            case AiAggressiveMode.PlayerThree:
                return PlayerThreeInput;
            case AiAggressiveMode.PlayerFour:
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
        //TODO: Make a method that returns layout
        {
            var layouts = SpawnLayouts.Where(spawnLayout => spawnLayout.Positions.Length >= biggestTeam).ToList();
            layout = layouts[Ran.Range(0, layouts.Count)];
        }

        for (var i = 0; i < biggestTeam; i++)
        {
            if (redTeamAmount > i)
                SpawnPlayer(GameSettings.Instance.RedTeamComposition[i], layout.Positions[i], TeamManager.Instance.RedTeam,
                    GameSettings.Instance.RedTeam[GameSettings.Instance.RedTeamComposition[i]].Name);
            if (blueTeamAmount > i)
                SpawnPlayer(GameSettings.Instance.BlueTeamComposition[i], -layout.Positions[i], TeamManager.Instance.BlueTeam,
                    GameSettings.Instance.BlueTeam[GameSettings.Instance.BlueTeamComposition[i]].Name);
        }
    }

    void SpawnBall()
    {
        Instantiate(prefabBall.gameObject, new Vector3(0, 30, 0), Quaternion.identity);
    }

    void SpawnPlayer(PlayerMoterInputBase player, Vector3 pos, TeamManager.TeamInstance team, string name = "")
    {
        var newPlayer = Instantiate(prefabMoter.gameObject, pos, Quaternion.identity, PlayersFolder);
        var newPlayerMoter = newPlayer.GetComponent<PlayerMoter>();
        newPlayerMoter.SetInput(player);
        newPlayerMoter.SetTeam(team.team.myTeam);
        newPlayerMoter.SetName(name);
        newPlayerMoter.OnSpawn();
        TeamManager.Instance.players.Add(new TeamManager.PlayerInstance(newPlayerMoter));
    }


    void SetUpCamera()
    {
        var cameras = FindObjectsOfType<CameraLookAtTargetOverride>();
        for (var i = cameras.Length - 1; i >= 0; i--)
            cameras[i].camera.depth = 20 + i;

        //Setup the differant camers screen size
        switch (cameras.Length)
        {
            case 1:
                Camera.main.enabled = false;
                return;
            case 12:
                cameras[11].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveLower1000);
                cameras[10].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveLower0100);
                cameras[9].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveLower0010);
                cameras[8].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveLower0001);
                cameras[7].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveMiddl1000);
                cameras[6].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveMiddl0100);
                cameras[5].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveMiddl0010);
                cameras[4].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveMiddl0001);
                cameras[3].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveUpper1000);
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveUpper0100);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveUpper0010);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TelveUpper0001);
                break;
            case 11:
                cameras[10].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.MiddleOfScreenMiny);
                cameras[9].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper10000);
                cameras[8].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper01000);
                cameras[7].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper00100);
                cameras[6].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper00010);
                cameras[5].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper00001);
                cameras[4].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower10000);
                cameras[3].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower01000);
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower00100);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower00010);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower00001);
                break;
            case 10:
                cameras[9].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper10000);
                cameras[8].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper01000);
                cameras[7].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper00100);
                cameras[6].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper00010);
                cameras[5].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenUpper00001);
                cameras[4].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower10000);
                cameras[3].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower01000);
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower00100);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower00010);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TenLower00001);
                break;
            case 9:
                cameras[8].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineUpper100);
                cameras[7].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineUpper010);
                cameras[6].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineUpper001);
                cameras[5].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineMiddl100);
                cameras[4].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineMiddl010);
                cameras[3].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineMiddl001);
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineLower100);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineLower010);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineLower001);
                break;
            case 8:
                cameras[7].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineUpper100);
                cameras[6].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineUpper010);
                cameras[5].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineUpper001);
                cameras[4].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineMiddl100);
                cameras[3].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineMiddl001);
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineLower100);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineLower010);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.NineLower001);
                break;
            case 7:
                cameras[6].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.MiddleOfScreenSmall);
                cameras[5].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixUpper10);
                cameras[4].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixUpper01);
                cameras[3].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixMiddl10);
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixMiddl01);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixLower10);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixLower01);
                break;
            case 6:
                cameras[5].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixUpper10);
                cameras[4].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixUpper01);
                cameras[3].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixMiddl10);
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixMiddl01);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixLower10);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.SixLower01);
                break;
            case 5:
                cameras[4].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.MiddleOfScreen);
                cameras[3].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerRightLower);
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerRightUpper);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerLeftLower);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerLeftUpper);
                break;
            case 4:
                cameras[3].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerRightLower);
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerRightUpper);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerLeftLower);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerLeftUpper);
                break;
            case 3:
                cameras[2].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.ThreePlayerLowerMiddle);
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerRightUpper);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.FourPlayerLeftUpper);
                break;
            case 2:
                cameras[1].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TwoPlayerLeft);
                cameras[0].camera.rect = CamUtils.SetCameraRect(CamUtils.CameraMode.TwoPlayerRight);
                break;
        }
    }
    //Used for making it easy to see where the spawn points are while editing them
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