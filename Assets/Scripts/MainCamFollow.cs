using UnityEngine;

public class MainCamFollow : JMilesBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (!target)
            if(FindObjectOfType<Ball>())
                target = FindObjectOfType<Ball>().transform;
        if (target)
            transform.LookAt(target);
    }
}
