using System;
using UnityEngine.UI;

public class SliderEvent: JMilesBehaviour
{
	public Slider        slider;
	public Action<float> onValueChanged;
	public float Value
	{
		get { return slider.value; }
		set { slider.value = value; }
	}

	private void OnEnable()
	{
		if(slider == null)
			slider = GetComponent<Slider>();

		slider.onValueChanged.AddListener(ValueChanged);
	}

	private void OnDisable()
	{
		slider.onValueChanged.RemoveListener(ValueChanged);
	}

	private void ValueChanged(float value)
	{
		onValueChanged.Trigger(value);
	}
}