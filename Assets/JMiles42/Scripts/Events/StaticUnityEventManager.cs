using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class StaticUnityEventManager
{
	private static readonly Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

	public static void StartListening(string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		if(eventDictionary.TryGetValue(eventName, out thisEvent))
			thisEvent.AddListener(listener);
		else
		{
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			eventDictionary.Add(eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		if(eventDictionary.TryGetValue(eventName, out thisEvent))
			thisEvent.RemoveListener(listener);
	}

	public static void TriggerEvent(string eventName)
	{
		UnityEvent thisEvent = null;

		if(eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke();
			Debug.Log(string.Format("Triggered \"{0}\" Event", eventName));
		}
	}

	public static void StartListening(PlayerInputValues eventName, PlayerInputDirections eventDir, UnityAction listener)
	{
		StartListening(eventName + eventDir.ToString(), listener);
	}

	public static void StopListening(PlayerInputValues eventName, PlayerInputDirections eventDir, UnityAction listener)
	{
		StopListening(eventName + eventDir.ToString(), listener);
	}

	public static void TriggerEvent(PlayerInputValues eventName, PlayerInputDirections eventDir)
	{
		TriggerEvent(eventName + eventDir.ToString());
	}

	public static void StartListening(PlayerInputValues eventName, UnityAction listener)
	{
		StartListening(eventName.ToString(), listener);
	}

	public static void StopListening(PlayerInputValues eventName, UnityAction listener)
	{
		StopListening(eventName.ToString(), listener);
	}

	public static void TriggerEvent(PlayerInputValues eventName)
	{
		TriggerEvent(eventName.ToString());
	}
}