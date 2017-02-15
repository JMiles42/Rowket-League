using UnityEngine;
using System.Collections;
namespace JMiles42.Maths
{
	public class Maths
	{
		public static bool IsOdd(int value)
		{
			return value % 2 != 0;
		}
		public static bool IsEven(int value)
		{
			return !(value % 2 != 0);
		}
	}
}
