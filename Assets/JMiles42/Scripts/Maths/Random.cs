using UnityEngine;
using System.Collections;
using JMiles42.Maths;
namespace JMiles42.Maths
{
	public static class RandomStrings
	{
		public const string ALLALPHA = "abcdefghijklmnopqrstuvwxyz";
		public const string ALLNUMERIC = "1234567890";
		public const string SYMBOLS = "!@#$%^&*()-_=+`~,<.>/?;:'";
		public const string ALLCHARS = ALLALPHA+ALLNUMERIC;
		public const string NUMERICSYMBOLS = SYMBOLS+ALLNUMERIC;

		public static string GetRandomString(int length)
		{
			string s="";
			System.Random rand = new System.Random(System.DateTime.UtcNow.Millisecond);
			for( int i = 0; i < length; i++ )
			{
				s += ALLCHARS[rand.Next(ALLCHARS.Length)];
			}
			return s;
		}

		public static string GetRandomString(int length, int seed)
		{
			string s="";
			System.Random rand = new System.Random(seed);
			for( int i = 0; i < length; i++ )
			{
				s += ALLCHARS[rand.Next(ALLCHARS.Length)];
			}
			return s;
		}

		public static string GetRandomAltString(int length)
		{
			string s="";
			System.Random rand = new System.Random(System.DateTime.UtcNow.Millisecond);
			for( int i = 0; i < length; i++ )
			{
				if( Maths.IsOdd(i) )
					s += ALLNUMERIC[rand.Next(ALLNUMERIC.Length)];
				else
					s += ALLALPHA[rand.Next(ALLALPHA.Length)];
			}
			return s;
		}

		public static string GetRandomAltString(int length, int seed)
		{
			string s="";
			System.Random rand = new System.Random(seed);
			for( int i = 0; i < length; i++ )
			{
				if(Maths.IsOdd(i))
					s += ALLNUMERIC[rand.Next(ALLNUMERIC.Length)];
				else
					s += ALLALPHA[rand.Next(ALLALPHA.Length)];
			}
			return s;
		}
	}
	public static class RandomBools
	{
		public static bool RandomBool()
		{
			return Maths.IsEven(System.DateTime.UtcNow.Millisecond);
		}
		public static bool RandomInvertedBool()
		{
			return Maths.IsOdd(System.DateTime.UtcNow.Millisecond);
		}
	}
}
