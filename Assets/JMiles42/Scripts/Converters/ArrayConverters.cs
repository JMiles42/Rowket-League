using System;
namespace JMiles42.Converters
{
	public static class ArrayConverter
	{
		/// <summary>
		/// Constent splitter
		/// </summary>
		const string SPLITSTRING = ":";
		/// <summary>
		/// The True symbol for bools
		/// </summary>
		const string TRUESTRING = "T";
		/// <summary>
		/// The False symbol for bools
		/// </summary>
		const string FALSESTRING = "F";
		/// <summary>
		/// Converts an Array of bools to a string
		/// </summary>
		/// <param name="from">The array to string</param>
		/// <returns>The array from string</returns>
		public static string BoolStringFromArray(bool[] from)
		{
			string to = "";
			for( int i = 0; i < from.Length; i++ )
			{
				if( from[i] )
					to += TRUESTRING + SPLITSTRING;
				else
					to += FALSESTRING + SPLITSTRING;
			}
			return to;
		}
		/// <summary>
		/// Converts an Array of bools string back to an array
		/// </summary>
		/// <param name="from">The array as string</param>
		/// <returns>The array from string</returns>
		public static bool[] BoolArrayFromString(string from)
		{
			string[] s = from.Split(new string[] { SPLITSTRING }, StringSplitOptions.None);
			bool[] to = new bool[s.Length];
			for( int i = 0; i < s.Length; i++ )
			{
				if( s[i] == TRUESTRING )
					to[i] = true;
				else if( s[i] == FALSESTRING )
					to[i] = false;
			}
			return to;
		}
		/// <summary>
		/// Converts an Array of floats to a string
		/// </summary>
		/// <param name="from">The array to string</param>
		/// <returns>The array from string</returns>
		public static string FloatStringFromArray(float[] from)
		{
			string to = "";
			for( int i = 0; i < from.Length; i++ )
			{
				to += from[i].ToString() + SPLITSTRING;
			}
			return to;
		}
		/// <summary>
		/// Converts an Array of floats string back to an array
		/// </summary>
		/// <param name="from">The array as string</param>
		/// <returns>The array from string</returns>
		public static float[] FloatArrayFromString(string from)
		{
			string[] s = from.Split(new string[] { SPLITSTRING }, StringSplitOptions.None);
			float[] to = new float[s.Length-1];
			for( int i = 0; i < s.Length - 1; i++ )
			{
				to[i] = float.Parse(s[i]);
			}
			return to;
		}
	}
}
