using UnityEngine;

public class KillMe: MonoBehaviour
{
	public float lifeTime = 10f; //My lifetime

	private void Start()
	{
		Invoke("Kill", lifeTime); //Call Kill in lifeTime seconds
	}

	private void Kill()
	{
		Destroy(gameObject); //Destroy object
	}
}