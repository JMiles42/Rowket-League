using System.Collections;
using UnityEngine;

public class JMilesBehaviour : MonoBehaviour
{
    private Transform m_Transform;
    public new Transform transform
    {
        get
        {
            if (!m_Transform)
                m_Transform = GetComponent<Transform>();
            return m_Transform;
        }
        set { m_Transform = value; }
    }
    public Quaternion Rotation
    {
        get
        {
            return transform.rotation;
        }

        set
        {
            transform.rotation = value;
        }
    }
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }

        set
        {
            transform.position = value;
        }
    }
    
    public void StartRoutine(IEnumerator method)
    {
        StartCoroutine(method);
    }

    public void StopRoutine(IEnumerator method)
    {
        StopCoroutine(method);
    }
}
