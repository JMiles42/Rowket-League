using UnityEngine.SceneManagement;

public class SimpleTimedScene: JMilesBehaviour
{
	public int   levelNum;
	public float timeLoad;

	public void Start()
	{
		Invoke("LevelLoad", timeLoad);
	}

	private void LevelLoad()
	{
		SceneManager.LoadScene(levelNum);
	}
}