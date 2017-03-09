using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JMiles42.Data;
using UnityEngine.UI;

public class PlayerInputManager : Singleton<PlayerInputManager>
{
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

    public List<InputAxis> inputsToUse = new List<InputAxis>
    {
        new InputAxis("Horizontal"),
        new InputAxis("Vertical"),
        new InputAxis("MouseScroll"),
        new InputAxis("Jump")
    };
    public Vector2 MousePos;

	void Start()
	{
        DontDestroyOnLoad(this);
    }
	void Update()
	{
		GetAxisValues();
		TriggerInputs();
	}
	void GetAxisValues()
	{
	    foreach (InputAxis input in inputsToUse) input.UpdateData();
	    MousePos = Input.mousePosition;
	}
	public void TriggerInputs()
	{
        if(inputsToUse.Count == 0) return;
		foreach (InputAxis key in inputsToUse)
		{
		    if (Input.GetButtonUp(key))
		    {
		        key.onKeyUp.Trigger();
		        StaticUnityEventManager.TriggerEvent(key + PlayerInputDirections.Up);
		    }
			else if (Input.GetButtonDown(key))
			{
			    key.onKeyDown.Trigger();
			    StaticUnityEventManager.TriggerEvent(key + PlayerInputDirections.Down);
			}
			else if (Input.GetButton(key))
			{
			    key.onKey.Trigger();
			    StaticUnityEventManager.TriggerEvent(key + PlayerInputDirections.Held);
			}
		}
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
    Sprint,
}
[Serializable]
public class InputAxis
{
    public string Axis;
    public float Value
    {
        get{ return ValueInverted ? m_Value : -m_Value;}
        set{ m_Value = ValueInverted ? value : -value;}
    }
    [SerializeField]
    float m_Value = 0;
    public bool ValueInverted;

    public Action onKeyDown;
    public Action onKeyUp;
    public Action onKey;

    public InputAxis(string axis, bool invert = false)
    {
        Axis = axis;
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
