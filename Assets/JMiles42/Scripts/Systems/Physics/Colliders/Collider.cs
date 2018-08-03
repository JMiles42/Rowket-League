using System.Collections.Generic;
using UnityEngine;

namespace JMiles42.Physics
{
	public abstract class Collider: MonoBehaviour
	{
		public static List<Collider> ActiveColliders = new List<Collider>();
		private       PhysicalObject m_PhysicalObject;
		public PhysicalObject physicalObject
		{
			get
			{
				if(!m_PhysicalObject)
					m_PhysicalObject = GetComponent<PhysicalObject>();

				return m_PhysicalObject;
			}
			set { m_PhysicalObject = value; }
		}

		private void OnDisable()
		{
			ActiveColliders.Remove(this);
		}

		private void OnEnable()
		{
			ActiveColliders.Remove(this);
			ActiveColliders.Add(this);
		}

		public virtual bool VectorCollidingWithObject(Vector3 posToCheck)
		{
			return transform.position == posToCheck;
		}
	}
}