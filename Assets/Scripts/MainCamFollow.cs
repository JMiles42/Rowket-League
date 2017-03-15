using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamFollow : JMilesBehaviour
{
    void LateUpdate()
    {
        var target = FindObjectOfType<Ball>();
        if (target)
            transform.LookAt(target.transform);
    }
}
