using System;
using JMiles42.UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickEvent: JMilesBehaviour
{
	public ButtonText myButton;
	public Action     onMouseClick;

	private void OnEnable()
	{
		if(myButton.Btn == null)
			myButton.OnEnable(gameObject);

		myButton.Btn.onClick.AddListener(MouseClick);
	}

	private void OnDisable()
	{
		myButton.Btn.onClick.RemoveListener(MouseClick);
	}

	private void MouseClick()
	{
		onMouseClick.Trigger();
	}
}