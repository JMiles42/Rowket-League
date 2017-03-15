using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomNameInputFeild : JMilesBehaviour
{
    public InputFieldEvent inputFeild;
    public StringListScriptableObject NameList;

    void Start()
    {
        if (inputFeild.Text == "")
            inputFeild.Text = NameList.GetRandomEntry();
    }
}
