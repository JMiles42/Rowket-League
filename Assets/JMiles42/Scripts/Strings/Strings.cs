using JMiles42.Maths;
using UnityEngine;

namespace JMiles42
{
	public static class Strings
	{
		public static string GetStringWithColourTag(string data, Color col)
		{
			return string.Format("<color={0}>{1}</color>", ConvertColours.HexNumberFromColour(col), data);
		}

		public static string GetStringWithSizeTag(string data, int size)
		{
			return string.Format("<size={0}>{1}</size>", size, data);
		}

		public static string GetStringWithSizeAndColourTag(string data, int size, Color col)
		{
			return GetStringWithColourTag(GetStringWithSizeTag(data, size), col);
		}
	}
}