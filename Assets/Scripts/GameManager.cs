using System;
using System.Collections;
using System.Linq;
using JMiles42.Data;
using UnityEngine;
using Ran = UnityEngine.Random;
using GameSettings = GameSettingsManagerMaster;
using CamUtils = SplitScreenCamUtils;

/// <summary>
///     The game manager, Controls the gamemode, and misc game logic
/// </summary>
public class GameManager: Singleton<GameManager>
{
	public Action<float>              onGameCountdown;
	public Action                     onGameStartCountdown;
	public Action                     onGameStart;
	public Action                     onGameEnd;
	public Action                     onGameInputEnable;
	public Action                     onGameInputDisable;
	public Action                     onAnyGoal;
	public StringListScriptableObject AiNames;
	public SpawnLayout[]              SpawnLayouts;
	public PlayerMotorInputAI[]       AiInputSystems;
	public PlayerMotorInputUser       PlayerOneInput;
	public PlayerMotorInputUser       PlayerTwoInput;
	public PlayerMotorInputUser       PlayerThreeInput;
	public PlayerMotorInputUser       PlayerFourInput;
	public PlayerMotor                prefabMotor;
	public Ball                       prefabBall;
	public ButtonClickEvent           startGameBtn;
	public Transform                  PlayersFolder;
	public float                      TimerMax;
	public Transform                  lookAtScoreBoard;

	private void OnEnable()
	{
		onAnyGoal                                 += GameOver;
		PlayerMotorInputUser.lookAtTargetOverride =  lookAtScoreBoard;
		onAnyGoal                                 += () => { PlayerMotorInputUser.lookAtOverride = true; };
		onGameStart                               += () => { PlayerMotorInputUser.lookAtOverride = false; };
		onGameStart                               += EnableInput;
		onGameEnd                                 += DisableInput;
		startGameBtn.onMouseClick                 += StartGame;
		var goals = FindObjectsOfType<Goal>();

		foreach(var goal in goals)
			goal.onGoal += CallAnyGoal;
	}

	private void OnDisable()
	{
		onAnyGoal                 -= GameOver;
		onGameStart               -= EnableInput;
		onGameEnd                 -= DisableInput;
		startGameBtn.onMouseClick -= StartGame;
		var goals = FindObjectsOfType<Goal>();

		foreach(var goal in goals)
			goal.onGoal -= CallAnyGoal;
	}

	private void CallAnyGoal()
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
		var timer = TimerMax;

		while(timer > 0)
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
		foreach(var reset in ResetableObjectBase.ResetableObjects)
			reset.Reset();

		RestartGame();
	}

	private IEnumerator GameEnd()
	{
		yield return WaitForTimes.GetWaitForTime(3);

		Restart();
	}

	public PlayerMotorInputBase GetInputClass(InputMoterMode moterMode, AiReactionTime time)
	{
		//Check if it is a player
		switch(moterMode)
		{
			case InputMoterMode.PlayerOne:   return PlayerOneInput;
			case InputMoterMode.PlayerTwo:   return PlayerTwoInput;
			case InputMoterMode.PlayerThree: return PlayerThreeInput;
			case InputMoterMode.PlayerFour:  return PlayerFourInput;
		}

		for(int i = 0, j = AiInputSystems.Length; i < j; i++)
		{
			if((AiInputSystems[i].AiMoterMode == moterMode) && (AiInputSystems[i].ReactionTime == time))
				return AiInputSystems[i];
		}

		return AiInputSystems[Ran.Range(0, AiInputSystems.Length)];
	}

	private void SpawnPlayers()
	{
		var         TeamOneAmount = GameSettings.Instance.TeamOneComposition.Length;
		var         TeamTwoAmount = GameSettings.Instance.TeamTwoComposition.Length;
		var         biggestTeam   = Mathf.Max(TeamTwoAmount, TeamOneAmount);
		SpawnLayout _layout;

		//TODO: Make a method that returns layout
		{
			var layouts = SpawnLayouts.Where(spawnLayout => spawnLayout.Positions.Length >= biggestTeam).ToList();
			_layout = layouts[Ran.Range(0, layouts.Count)];
		}

		for(var i = 0; i < biggestTeam; i++)
		{
			if(TeamOneAmount > i)
			{
				SpawnPlayer(GameSettings.Instance.TeamOneComposition[i], -_layout.Positions[i], TeamManager.Instance.TeamOne, GameSettings.Instance.TeamOne[GameSettings.Instance.TeamOneComposition[i]].Name);
			}

			if(TeamTwoAmount > i)
			{
				SpawnPlayer(GameSettings.Instance.TeamTwoComposition[i], _layout.Positions[i], TeamManager.Instance.TeamTwo, GameSettings.Instance.TeamTwo[GameSettings.Instance.TeamTwoComposition[i]].Name);
			}
		}
	}

	private void SpawnBall()
	{
		Instantiate(prefabBall.gameObject, new Vector3(0, 30, 0), Quaternion.identity);
	}

	private void SpawnPlayer(PlayerMotorInputBase player, Vector3 pos, TeamManager.TeamInstance team, string _name = "")
	{
		var newPlayer      = Instantiate(prefabMotor.gameObject, pos, Quaternion.identity, PlayersFolder);
		var newPlayerMotor = newPlayer.GetComponent<PlayerMotor>();
		newPlayerMotor.SetInput(player);
		newPlayerMotor.SetTeam(team.team.myTeam);
		newPlayerMotor.SetName(_name);
		newPlayerMotor.OnSpawn();
		TeamManager.Instance.players.Add(new TeamManager.PlayerInstance(newPlayerMotor));
	}

	private void SetUpCamera()
	{
		var cameras = FindObjectsOfType<CameraLookAtTargetOverride>();

		for(var i = cameras.Length - 1; i >= 0; i--)
			cameras[i].camera.depth = 20 + i;

		//Setup the differant camers screen size

		//How many player cameras are there
		switch(cameras.Length)
		{
			case 1:
				Camera.main.enabled = false;

				return;
			case 12:
				cameras[11].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveLower1000);
				cameras[10].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveLower0100);
				cameras[9].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveLower0010);
				cameras[8].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveLower0001);
				cameras[7].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveMiddl1000);
				cameras[6].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveMiddl0100);
				cameras[5].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveMiddl0010);
				cameras[4].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveMiddl0001);
				cameras[3].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveUpper1000);
				cameras[2].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveUpper0100);
				cameras[1].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveUpper0010);
				cameras[0].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TelveUpper0001);

				break;
			case 11:
				cameras[10].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.MiddleOfScreenMiny);
				cameras[9].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper10000);
				cameras[8].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper01000);
				cameras[7].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper00100);
				cameras[6].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper00010);
				cameras[5].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper00001);
				cameras[4].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower10000);
				cameras[3].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower01000);
				cameras[2].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower00100);
				cameras[1].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower00010);
				cameras[0].camera.rect  = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower00001);

				break;
			case 10:
				cameras[9].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper10000);
				cameras[8].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper01000);
				cameras[7].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper00100);
				cameras[6].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper00010);
				cameras[5].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenUpper00001);
				cameras[4].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower10000);
				cameras[3].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower01000);
				cameras[2].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower00100);
				cameras[1].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower00010);
				cameras[0].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TenLower00001);

				break;
			case 9:
				cameras[8].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineUpper100);
				cameras[7].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineUpper010);
				cameras[6].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineUpper001);
				cameras[5].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineMiddl100);
				cameras[4].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineMiddl010);
				cameras[3].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineMiddl001);
				cameras[2].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineLower100);
				cameras[1].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineLower010);
				cameras[0].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineLower001);

				break;
			case 8:
				cameras[7].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineUpper100);
				cameras[6].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineUpper010);
				cameras[5].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineUpper001);
				cameras[4].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineMiddl100);
				cameras[3].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineMiddl001);
				cameras[2].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineLower100);
				cameras[1].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineLower010);
				cameras[0].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.NineLower001);

				break;
			case 7:
				cameras[6].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.MiddleOfScreenSmall);
				cameras[5].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixUpper10);
				cameras[4].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixUpper01);
				cameras[3].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixMiddl10);
				cameras[2].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixMiddl01);
				cameras[1].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixLower10);
				cameras[0].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixLower01);

				break;
			case 6:
				cameras[5].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixUpper10);
				cameras[4].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixUpper01);
				cameras[3].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixMiddl10);
				cameras[2].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixMiddl01);
				cameras[1].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixLower10);
				cameras[0].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.SixLower01);

				break;
			case 5:
				cameras[4].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.MiddleOfScreen);
				cameras[3].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerRightLower);
				cameras[2].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerRightUpper);
				cameras[1].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerLeftLower);
				cameras[0].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerLeftUpper);

				break;
			case 4:
				cameras[3].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerRightLower);
				cameras[2].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerRightUpper);
				cameras[1].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerLeftLower);
				cameras[0].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerLeftUpper);

				break;
			case 3:
				cameras[2].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.ThreePlayerLowerMiddle);
				cameras[1].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerRightUpper);
				cameras[0].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.FourPlayerLeftUpper);

				break;
			case 2:
				cameras[1].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TwoPlayerLeft);
				cameras[0].camera.rect = CamUtils.SetCameraRect(SplitScreenCamUtils.CameraMode.TwoPlayerRight);

				break;
		}
	}

	public static bool GetIsPlayer(InputMoterMode mode)
	{
		switch(mode)
		{
			case InputMoterMode.PlayerOne:   return true;
			case InputMoterMode.PlayerTwo:   return true;
			case InputMoterMode.PlayerThree: return true;
			case InputMoterMode.PlayerFour:  return true;
			default:                         return false;
		}
	}
#if UNITY_EDITOR
	//Used for making it easy to see where the spawn points are while editing them
	public SpawnLayout layout;
	private void OnDrawGizmos()
	{
		if(!layout)
			return;

		if(layout.Positions.Length <= 0)
			return;

		foreach(var v in layout.Positions)
			Gizmos.DrawSphere(v, 1);
	}
#endif
}