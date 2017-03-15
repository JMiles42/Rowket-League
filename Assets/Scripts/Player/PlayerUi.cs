using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : JMilesBehaviour
{
    public Canvas canvis;
    public Text NameDisplay;
    public PlayerMoter moter;

    void Start()
    {
        NameDisplay.text = moter.playerName;
        canvis.transform.SetParent(moter.transform.parent);
    }

    private void LateUpdate()
    {
        canvis.transform.LookAt(Camera.main.transform,transform.up);
        canvis.transform.position = moter.Position + (Vector3.up * 3);
    }
}
