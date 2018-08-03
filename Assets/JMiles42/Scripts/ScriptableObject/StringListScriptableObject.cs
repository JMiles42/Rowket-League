using UnityEngine;
using Ran = UnityEngine.Random;

[CreateAssetMenu(fileName = "String List", menuName = "JMiles42/String List", order = 0)]
public class StringListScriptableObject: JMilesScriptableObject
{
	public string[] Strings;

	public string GetRandomEntry()
	{
		return Strings[Ran.Range(0, Strings.Length)];
	}
}