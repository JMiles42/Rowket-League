using System;

namespace JMiles42.Generics
{
	public static class Arrays
	{
		public static T[] ShuffleArray<T>(T[] array, int seed)
		{
			var prng = new Random(seed);

			for(var i = 0; i < array.Length; i++)
			{
				var randomIndex = prng.Next(i, array.Length);
				var tempItem    = array[randomIndex];
				array[randomIndex] = array[i];
				array[i]           = tempItem;
			}

			return array;
		}
	}
}