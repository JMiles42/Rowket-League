using UnityEngine;
using System.Collections;
using System.Text;
using JMiles42.Maths;

namespace JMiles42.Maths
{
    public static class RandomStrings
    {
        public const string ALLALPHA = "abcdefghijklmnopqrstuvwxyz";
        public const string ALLNUMERIC = "1234567890";
        public const string SYMBOLS = "!@#$%^&*()-_=+`~,<.>/?;:'";
        public const string ALLCHARS = ALLALPHA + ALLNUMERIC;
        public const string NUMERICSYMBOLS = SYMBOLS + ALLNUMERIC;

        public static string GetRandomString(int length)
        {
            var s = new StringBuilder();
            var rand = new System.Random(System.DateTime.UtcNow.Millisecond);
            for (var i = 0; i < length; i++)
                s.Append(ALLCHARS[rand.Next(ALLCHARS.Length)]);
            return s.ToString();
        }

        public static string GetRandomString(int length, int seed)
        {
            var s = new StringBuilder();
            var rand = new System.Random(seed);
            for (var i = 0; i < length; i++)
            {
                s.Append(ALLCHARS[rand.Next(ALLCHARS.Length)]);
            }
            return s.ToString();
        }

        public static string GetRandomAltString(int length)
        {
            var s = new StringBuilder();
            var rand = new System.Random(System.DateTime.UtcNow.Millisecond);
            for (var i = 0; i < length; i++)
                s.Append(Maths.IsOdd(i)
                    ? ALLNUMERIC[rand.Next(ALLNUMERIC.Length)]
                    : ALLALPHA[rand.Next(ALLALPHA.Length)]);

            return s.ToString();
        }

        public static string GetRandomAltString(int length, int seed)
        {
            var s = new StringBuilder();
            var rand = new System.Random(seed);
            for (int i = 0; i < length; i++)
                s.Append(Maths.IsOdd(i)
                    ? ALLNUMERIC[rand.Next(ALLNUMERIC.Length)]
                    : ALLALPHA[rand.Next(ALLALPHA.Length)]);
            return s.ToString();
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