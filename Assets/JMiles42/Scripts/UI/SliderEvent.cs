using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderEvent : JMilesBehaviour
{
    public Slider slider;
    public float Value
    {
        get { return slider.value; }
        set { slider.value = value; }
    }
    public Action<float> onValueChanged;

    private void OnEnable()
    {
        if (slider == null) slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ValueChanged);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ValueChanged);
    }

    void ValueChanged(float value)
    {
        onValueChanged.Trigger(value);
    }
}
