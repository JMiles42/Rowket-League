using UnityEngine;
public class KillMe : MonoBehaviour
{
	public float lifeTime = 10f;//My lifetime
	void Start ()
	{
		Invoke ("Kill", lifeTime);//Call Kill in lifeTime seconds
	}
	void Kill ()
	{
		Destroy (this.gameObject);//Destroy object
	}
}
