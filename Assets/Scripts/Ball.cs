using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : JMilesBehaviour {

    private Rigidbody m_Rigidbody;
    public new Rigidbody Rigidbody
    {
        get
        {
            if (!m_Rigidbody)
                m_Rigidbody = GetComponent<Rigidbody>();
            return m_Rigidbody;
        }
        set { m_Rigidbody = value; }
    }

    // Use this for initialization
	void Start ()
	{
	    Rigidbody.velocity = Vector3.down * 20f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
