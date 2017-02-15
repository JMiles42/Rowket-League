using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SimpleTurntableRotate : JMilesBehaviour
{
    public Vector3 rotateAngle;
    public Transform transformToMove;
    public Space transformSpace;

    void Start()
    {
        if (!transformToMove) transformToMove = GetComponent<Transform>();
    }

    void Update ()
    {
        transformToMove.Rotate(rotateAngle * Time.deltaTime, transformSpace);
	}
}
