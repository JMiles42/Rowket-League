using UnityEngine.SceneManagement;

public class SimpleSceneLoader: JMilesBehaviour
{
	public int levelNum;

	public void LoadScene()
	{
		SceneManager.UnloadSceneAsync(levelNum);
		SceneManager.LoadScene(levelNum);
	}

	public void LoadScene(int s)
	{
		SceneManager.LoadScene(s);
	}

	public void LoadScene(string s)
	{
		SceneManager.LoadScene(s);
	}

	public static void ResetGame()
	{
		SceneManager.LoadScene(0);
	}

	public static void EndGame()
	{
		SceneManager.LoadScene(2);
	}

	public static void LoadGame(int lvl)
	{
		SceneManager.LoadScene(lvl);
	}
}