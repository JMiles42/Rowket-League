using UnityEngine;

namespace JMiles42.Physics
{
	[RequireComponent(typeof(PhysicalObject))]
	public class BallCollider: Collider
	{
		public Vector3 centerOfSphere = Vector3.zero;
		public float   radius         = 0.5f;

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;

			if(physicalObject.collidingWithObject)
				Gizmos.color = Color.red;
			else if(!physicalObject.detectCollisions)
				Gizmos.color = Color.cyan;

			Gizmos.DrawWireSphere(centerOfSphere + transform.position, radius);
		}

		public override bool VectorCollidingWithObject(Vector3 posToCheck)
		{
			if(Vector3.Distance(transform.position, posToCheck) <= radius)
				return true;

			return false;

			//return transform.position == posToCheck;
		}
	}
}