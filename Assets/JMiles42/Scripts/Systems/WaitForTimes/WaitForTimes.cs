using UnityEngine;
using System.Collections.Generic;
using JMiles42.Data;

public class WaitForTimes : Singleton<WaitForTimes>
{
	public static WaitForFixedUpdate waitForFixedupdate;
	public static WaitForEndOfFrame waitForEndOfFrame;
	Dictionary<float, WaitForSeconds> waitForDictionary;

	void Awake()
	{
		if( waitForDictionary == null )
			waitForDictionary = new Dictionary<float, WaitForSeconds>();
		waitForFixedupdate = new WaitForFixedUpdate();
		waitForEndOfFrame = new WaitForEndOfFrame();
	}
	public static WaitForSeconds GetWaitForTime(float time)
	{
		if( Instance == null ) return null;

		WaitForSeconds thisWaitTime = null;
		if( Instance.waitForDictionary.TryGetValue(time, out thisWaitTime) )
		{
			return thisWaitTime;
		}
		else
		{
			thisWaitTime = new WaitForSeconds(time);
			Instance.waitForDictionary.Add(time, thisWaitTime);
		}
		return thisWaitTime;
	}
}
