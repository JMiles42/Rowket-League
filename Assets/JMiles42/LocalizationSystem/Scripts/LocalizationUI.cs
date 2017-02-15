using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationUI : MonoBehaviour
{
    public string LocalizationID;
    public Text txt;

    public void Start()
    {
        if (!txt) txt = GetComponent<Text>();
        txt.text = GetLocalizedData();
    }

    public string GetLocalizedData()
    {
        return LocalizationMasterComponent.Instance.GetDataFromKey(LocalizationID);
    }
}
