using System;
using System.Collections.Generic;
using System.Linq;
using JMiles42.Data;
using UnityEngine;

public class PlayerInputManager: Singleton<PlayerInputManager>
{
	public List<InputAxis> inputsToUse = new List<InputAxis> {new InputAxis("Horizontal"), new InputAxis("Vertical"), new InputAxis("MouseScroll"), new InputAxis("Jump"), new InputAxis("Fire1")};
	public Vector2         MousePos;
	public InputAxis Horizontal
	{
		get { return inputsToUse[0]; }
		set { inputsToUse[0] = value; }
	}
	public InputAxis Vertical
	{
		get { return inputsToUse[1]; }
		set { inputsToUse[1] = value; }
	}
	public InputAxis MouseScroll
	{
		get { return inputsToUse[2]; }
		set { inputsToUse[2] = value; }
	}
	public InputAxis Jump
	{
		get { return inputsToUse[3]; }
		set { inputsToUse[3] = value; }
	}
	public InputAxis Fire1
	{
		get { return inputsToUse[4]; }
		set { inputsToUse[4] = value; }
	}

	private void Start()
	{
		DontDestroyOnLoad(this);
	}

	private void Update()
	{
		GetAxisValues();
		TriggerInputs();
	}

	private void GetAxisValues()
	{
		foreach(var input in inputsToUse)
			input.UpdateData();

		MousePos = Input.mousePosition;
	}

	public void TriggerInputs()
	{
		if(inputsToUse.Count == 0)
			return;

		foreach(var key in inputsToUse)
		{
			if(Input.GetButtonUp(key))
			{
				key.onKeyUp.Trigger();
				//StaticUnityEventManager.TriggerEvent(key + PlayerInputDirections.Up);
			}
			else if(Input.GetButtonDown(key))
			{
				key.onKeyDown.Trigger();
				//StaticUnityEventManager.TriggerEvent(key + PlayerInputDirections.Down);
			}

			if(key != 0f)
			{
				key.onKey.Trigger();
				//StaticUnityEventManager.TriggerEvent(key + PlayerInputDirections.Held);
			}
		}
	}

	public static InputAxis GetAxisFromString(string axis)
	{
		return Instance.inputsToUse.FirstOrDefault(input => input == axis);
	}
}

public enum PlayerInputDirections
{
	Down,
	Up,
	Held
}

public enum PlayerInputValues
{
	Horizontal,
	Vertical,
	MouseScrollWheel,
	MouseY,
	MouseX,
	Jump,
	Sprint
}

[Serializable]
public class InputAxis
{
	public                   string Axis;
	[SerializeField] private float  m_Value;
	public                   bool   ValueInverted;
	public                   Action onKeyDown;
	public                   Action onKeyUp;
	public                   Action onKey;
	public float Value
	{
		get { return ValueInverted? m_Value : -m_Value; }
		set { m_Value = ValueInverted? value : -value; }
	}

	public InputAxis(string axis, bool invert = false)
	{
		Axis          = axis;
		ValueInverted = invert;
	}

	public void UpdateData()
	{
		m_Value = Input.GetAxis(Axis);
	}

	public static implicit operator float(InputAxis fp)
	{
		return fp.Value;
	}

	public static implicit operator string(InputAxis fp)
	{
		return fp.Axis;
	}

	public static implicit operator bool(InputAxis fp)
	{
		return fp.ValueInverted;
	}
}