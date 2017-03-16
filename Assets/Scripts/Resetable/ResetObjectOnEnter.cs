using UnityEngine;

/// <summary>
/// Resets any object on contact
/// Can be either a Trigger or Collider
/// </summary>
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

    private static void ResetGameobject(GameObject gO)
    {
        if (gO.GetComponent<ResetableObjectBase>())
            gO.GetComponent<ResetableObjectBase>().Reset();
        else if (gO.GetComponentInParent<ResetableObjectBase>())
            gO.GetComponentInParent<ResetableObjectBase>().Reset();
    }
}