using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class DropDownEvent: JMilesBehaviour
{
	public Dropdown    myDropdown;
	public Action<int> onValueChanged;
	public int Value
	{
		get { return myDropdown.value; }
		set { myDropdown.value = value; }
	}

	private void OnEnable()
	{
		if(myDropdown == null)
			myDropdown = GetComponent<Dropdown>();

		myDropdown.onValueChanged.AddListener(ValueChanged);
	}

	private void OnDisable()
	{
		myDropdown.onValueChanged.RemoveListener(ValueChanged);
	}

	private void ValueChanged(int value)
	{
		onValueChanged.Trigger(value);
	}
}