using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : JMilesBehaviour
{
    public Canvas canvis;
    public Text NameDisplay;
    public PlayerMoter moter;
    public Transform target;

    void Start()
    {
        //if (!target)
        //    target = Camera.main.transform;
        NameDisplay.text = moter.playerName;
        canvis.transform.SetParent(moter.transform.parent);
    }

    private void LateUpdate()
    {
        if (!target) return;
        canvis.transform.LookAt(Camera.main.transform,transform.up);
        canvis.transform.position = moter.Position + (Vector3.up * 3);
    }
}
