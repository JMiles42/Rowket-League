using System;

namespace JMiles42.Converters
{
	public static class ArrayConverter
	{
		/// <summary>
		///     Constent splitter
		/// </summary>
		private const string SPLIT_STRING = ":";
		/// <summary>
		///     The True symbol for bools
		/// </summary>
		private const string TRUE_STRING = "T";
		/// <summary>
		///     The False symbol for bools
		/// </summary>
		private const string FALSE_STRING = "F";

		/// <summary>
		///     Converts an Array of bools to a string
		/// </summary>
		/// <param name="from">The array to string</param>
		/// <returns>The array from string</returns>
		public static string BoolStringFromArray(bool[] from)
		{
			var to = "";

			for(var i = 0; i < from.Length; i++)
			{
				if(from[i])
					to += TRUE_STRING + SPLIT_STRING;
				else
					to += FALSE_STRING + SPLIT_STRING;
			}

			return to;
		}

		/// <summary>
		///     Converts an Array of bools string back to an array
		/// </summary>
		/// <param name="from">The array as string</param>
		/// <returns>The array from string</returns>
		public static bool[] BoolArrayFromString(string from)
		{
			var s  = from.Split(new[] {SPLIT_STRING}, StringSplitOptions.None);
			var to = new bool[s.Length];

			for(var i = 0; i < s.Length; i++)
			{
				if(s[i] == TRUE_STRING)
					to[i] = true;
				else if(s[i] == FALSE_STRING)
					to[i] = false;
			}

			return to;
		}

		/// <summary>
		///     Converts an Array of floats to a string
		/// </summary>
		/// <param name="from">The array to string</param>
		/// <returns>The array from string</returns>
		public static string FloatStringFromArray(float[] from)
		{
			var to = "";

			for(var i = 0; i < from.Length; i++)
				to += from[i] + SPLIT_STRING;

			return to;
		}

		/// <summary>
		///     Converts an Array of floats string back to an array
		/// </summary>
		/// <param name="from">The array as string</param>
		/// <returns>The array from string</returns>
		public static float[] FloatArrayFromString(string from)
		{
			var s  = from.Split(new[] {SPLIT_STRING}, StringSplitOptions.None);
			var to = new float[s.Length - 1];

			for(var i = 0; i < s.Length - 1; i++)
				to[i] = float.Parse(s[i]);

			return to;
		}
	}
}