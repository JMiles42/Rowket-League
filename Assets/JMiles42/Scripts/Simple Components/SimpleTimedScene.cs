using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SimpleTimedScene : JMilesBehaviour
{
	public int levelNum;
	public float timeLoad;

	public void Start()
	{
		Invoke("LevelLoad", timeLoad);
	}
	void LevelLoad()
	{
		SceneManager.LoadScene(levelNum);
	}
}
