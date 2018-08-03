using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JMilesRigidbodyBehaviour: JMilesBehaviour
{
	private Rigidbody m_Rigidbody;
	public new Rigidbody rigidbody
	{
		get
		{
			if(!m_Rigidbody)
				m_Rigidbody = GetComponent<Rigidbody>();

			return m_Rigidbody;
		}
		set { m_Rigidbody = value; }
	}
}