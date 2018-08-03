using System.Collections.Generic;
using UnityEngine.Events;

public static class EventManager
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
#if DEBUGCONSOLE
			Debug.Log(string.Format("Triggered : {0} Event", eventName));
			#endif
		}
	}
}