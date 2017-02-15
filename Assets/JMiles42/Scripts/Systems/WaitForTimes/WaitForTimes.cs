using UnityEngine;
using System.Collections.Generic;
using JMiles42.Data;

public static class WaitForTimes
{
	public static WaitForFixedUpdate waitForFixedupdate = new WaitForFixedUpdate();
	public static WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
	private static Dictionary<float, WaitForSeconds> waitForDictionary = new Dictionary<float, WaitForSeconds>();

	public static WaitForSeconds GetWaitForTime(float time)
	{
		WaitForSeconds thisWaitTime = null;
		if( waitForDictionary.TryGetValue(time, out thisWaitTime) )
		{
			return thisWaitTime;
		}
		else
		{
			thisWaitTime = new WaitForSeconds(time);
			waitForDictionary.Add(time, thisWaitTime);
		}
		return thisWaitTime;
	}
}
