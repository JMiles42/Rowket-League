using UnityEngine;

public class SimpleTurntableRotate: JMilesBehaviour
{
	public Vector3   rotateAngle;
	public Transform transformToMove;
	public Space     transformSpace;

	private void Start()
	{
		if(!transformToMove)
			transformToMove = GetComponent<Transform>();
	}

	private void Update()
	{
		transformToMove.Rotate(rotateAngle * Time.deltaTime, transformSpace);
	}
}