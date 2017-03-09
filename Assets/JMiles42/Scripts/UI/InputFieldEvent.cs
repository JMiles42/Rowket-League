﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldEvent : JMilesBehaviour
{
    public InputField inputField;
    public string Value
    {
        get { return inputField.text; }
        set { inputField.text = value; }
    }
    public Action<string> onValueChangedValue;
    public Action<string> onEndEdit;

    private void OnEnable()
    {
        if (inputField == null) inputField = GetComponent<InputField>();

        inputField.onValueChanged.AddListener(ValueChanged);
        inputField.onEndEdit.AddListener(EndEdit);
    }

    private void OnDisable()
    {
        inputField.onValueChanged.RemoveListener(ValueChanged);
        inputField.onEndEdit.RemoveListener(EndEdit);
    }

    void EndEdit(string value)
    {
        onEndEdit.Trigger(value);
    }

    void ValueChanged(string value)
    {
        onValueChangedValue.Trigger(value);
    }
}
