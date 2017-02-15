using UnityEngine;
namespace JMiles42.Maths
{
	#region Noise
	public class Noise
	{
		#region ReturnNoiseInt
		/// <summary>
		/// Randomise value by amount.
		/// </summary>
		/// <param name="value">Value to Randomize.</param>
		/// <param name="amount">Amount to Randomize.</param>
		/// <returns>Value +/- Amount</returns>
		public static int ReturnNoise (int value, int amount)
		{
			int f = Random.Range (value-amount, value+amount);
			return ( f );
		}
		#endregion
		#region ReturnNoiseFloat
		/// <summary>
		/// Randomise value by amount.
		/// </summary>
		/// <param name="value">Value to Randomize.</param>
		/// <param name="amount">Amount to Randomize.</param>
		/// <returns>Value +/- Amount</returns>
		public static float ReturnNoise (float value, float amount)
		{
			float f = Random.Range (value-amount, value+amount);
			return ( f );
		}
		#endregion
	}
	#endregion
}
