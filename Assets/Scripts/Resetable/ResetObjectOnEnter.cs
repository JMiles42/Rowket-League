using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectOnEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ResetGameobject(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        ResetGameobject(other.gameObject);
    }

    void ResetGameobject(GameObject gO)
    {
        if (gO.GetComponent<ResetableObject>())
            gO.GetComponent<ResetableObject>().Reset();
        else if(gO.GetComponentInParent<ResetableObject>())
            gO.GetComponentInParent<ResetableObject>().Reset();
        if (gO.GetComponent<ResetableObjectAdvanced>())
            gO.GetComponent<ResetableObjectAdvanced>().Reset();
        else if (gO.GetComponentInParent<ResetableObjectAdvanced>())
            gO.GetComponentInParent<ResetableObjectAdvanced>().Reset();
    }

}
