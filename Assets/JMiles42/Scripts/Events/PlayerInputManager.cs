using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JMiles42.Data;
using UnityEngine.UI;

public class PlayerInputManager : Singleton<PlayerInputManager>
{
    public InputAxis Horizontal;
    public InputAxis MouseScroll;
    public InputAxis Vertical;

	public Vector2 MousePos;

	void Start()
	{
        DontDestroyOnLoad(this);
    }
	void Update()
	{
		GetAxisValues();
		Axis();
		MousePress();
		TriggerInputs();
	}
	void GetAxisValues()
	{
        Horizontal.UpdateData();
        Vertical.UpdateData();
        MouseScroll.UpdateData();
		MousePos = Input.mousePosition;
	}
	void MousePress()
	{
		if (MouseScroll != 0) StaticUnityEventManager.TriggerEvent(PlayerInputValues.MouseScrollWheel);
	}
	void Axis()
	{
		if (Horizontal != 0) StaticUnityEventManager.TriggerEvent(PlayerInputValues.Horizontal);
		if (Vertical != 0) StaticUnityEventManager.TriggerEvent(PlayerInputValues.Vertical);
	}
	public List<string> inputsToUse;
	public void AddInput(string key)
	{
		if (!inputsToUse.Contains(key)) inputsToUse.Add(key);
	}
	public void RemoveInput(string key)
	{
		if (inputsToUse.Contains(key)) inputsToUse.Remove(key);
	}
	public void TriggerInputs()
	{
        if(inputsToUse.Count == 0) return;
		foreach (var k in inputsToUse)
		{
			if (Input.GetButtonUp(k))
				StaticUnityEventManager.TriggerEvent(k + PlayerInputDirections.Up);
			else if (Input.GetButtonDown(k))
				StaticUnityEventManager.TriggerEvent(k + PlayerInputDirections.Down);
			else if (Input.GetButton(k))
				StaticUnityEventManager.TriggerEvent(k + PlayerInputDirections.Held);
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
public struct InputAxis
{
    public string Axis;
    public float Value
    {
        get{ return ValueInverted ? m_Value : -m_Value;}
        set{ m_Value = ValueInverted ? value : -value;}
    }
    [SerializeField]
    float m_Value;
    public bool ValueInverted;

    public InputAxis(string axis,bool invert = false)
    {
        Axis = axis;
        m_Value = 0;
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
    public static implicit operator bool(InputAxis fp)
    {
        return fp.ValueInverted;
    }
}
