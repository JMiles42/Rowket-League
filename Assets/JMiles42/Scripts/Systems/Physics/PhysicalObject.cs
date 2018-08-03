using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JMiles42.Physics
{
	public class PhysicalObject: JMilesBehaviour
	{
		public static List<PhysicalObject> ActiveObjects = new List<PhysicalObject>();
		public static Vector3              DownGravity   = new Vector3(0, -10, 0);
		public        float                acceleration;
		public        Vector3              centerOfMass;
		public        bool                 detectCollisions = true;
		public        float                drag;
		public        float                friction;
		public        float                mass;
		public        bool                 useGravityFromObjects = true;
		public        bool                 useWorldGravity       = true;
		public        Vector3              velocity;
		public        Vector3              velocityOthers;
		private       Collider             m_collider;
		public new Collider collider
		{
			get
			{
				if(!m_collider)
					m_collider = GetComponent<Collider>();

				return m_collider;
			}
			set { m_collider = value; }
		}
		public Vector3 globalCenterOfMass
		{
			get { return transform.position + centerOfMass; }
		}
		public Vector3 finalVelocity
		{
			get
			{
				if(useWorldGravity)
					return velocity + (useGravityFromObjects? velocityOthers : Vector3.zero) + (DownGravity * acceleration * mass);

				return velocity + ((useGravityFromObjects? velocityOthers : Vector3.zero) * acceleration * mass);
			}
		}
		public bool collidingWithObject { get; private set; }

		private void OnDisable()
		{
			ActiveObjects.Remove(this);
		}

		private void OnEnable()
		{
			ActiveObjects.Remove(this);
			ActiveObjects.Add(this);
		}

		public void Start()
		{
			collidingWithObject = false;
			transform           = GetComponent<Transform>();
			collider            = GetComponent<Collider>();
		}

		private void FixedUpdate()
		{
			transform.position += finalVelocity          * Time.fixedDeltaTime;
			velocity           += (finalVelocity / drag) * Time.fixedDeltaTime;
			velocityOthers     =  Vector3.zero;
		}

		public void GravitateTo(PhysicalObject other) { }

		public Vector3 AngleTo(PhysicalObject other)
		{
			return Vector3.Cross(transform.position, other.transform.position);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(globalCenterOfMass, 0.02f);
			Handles.color = Color.magenta;

			if(finalVelocity.magnitude != 0f)
			{
				Handles.ArrowCap(0, globalCenterOfMass, Quaternion.LookRotation(finalVelocity * 360), finalVelocity.magnitude / 10);
			}
		}

		public bool VectorCollidingWithObject(Vector3 posToCheck)
		{
			if(collider)
				return collider.VectorCollidingWithObject(posToCheck);

			return transform.position == posToCheck;
		}
	}
}