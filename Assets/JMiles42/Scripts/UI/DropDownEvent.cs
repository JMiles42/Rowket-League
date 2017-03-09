using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class DropDownEvent : JMilesBehaviour
{
    public Dropdown myDropdown;

    public int Value
    {
        get { return myDropdown.value; }
        set { myDropdown.value = value; }
    }
    
    public Action<int> onValueChanged;

    private void OnEnable()
    {
        if( myDropdown == null ) myDropdown = GetComponent<Dropdown>();
        myDropdown.onValueChanged.AddListener(ValueChanged);
    }

    private void OnDisable()
    {
        myDropdown.onValueChanged.RemoveListener(ValueChanged);
    }

    void ValueChanged(int value)
    {
        onValueChanged.Trigger(value);
    }
}
