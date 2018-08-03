using UnityEngine;

namespace JMiles42.Physics
{
	public class GravityBall: MonoBehaviour
	{
		public float   Radius;
		public float   GravityStrength;
		public Vector3 lol;
		public Vector3 lol2;

		public void OnDrawGizmos()
		{
			Gizmos.DrawWireSphere(transform.position, Radius);
		}

		public void FixedUpdate()
		{
			foreach(var obj in PhysicalObject.ActiveObjects)
			{
				var hitDir = (transform.position - obj.Position).normalized;
				lol = hitDir;
				var hitPos = transform.position + (hitDir * Radius);
				lol2 = hitPos;

				//if (obj.VectorCollidingWithObject(hitPos))
				if(Vector3.Distance(transform.position, obj.Position) <= Radius)
					obj.velocityOthers += hitDir * GravityStrength;
			}
		}
	}
}