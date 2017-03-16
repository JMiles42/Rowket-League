using UnityEngine;

public class MainCamFollow : JMilesBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (!target)
            target = FindObjectOfType<Ball>().transform;
        if (target)
            transform.LookAt(target);
    }
}
