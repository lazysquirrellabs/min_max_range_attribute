using System;
using UnityEngine;

namespace SneakySquirrelLabs.MinMaxRangeAttribute
{
	/// <summary>
	/// An attribute that simplifies defining bounded ranges (ranges with a minimum and maximum) on the inspector.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class MinMaxRangeAttribute : PropertyAttribute
	{
		#region Fields

		public readonly float Min;
		public readonly float Max;
		public readonly uint Decimals;

		#endregion

		#region Setup

		/// <summary>
		/// A bounded range for integers.
		/// </summary>
		/// <param name="min">The minimum acceptable value.</param>
		/// <param name="max">The maximum acceptable value.</param>
		public MinMaxRangeAttribute(int min, int max)
		{
			Min = min;
			Max = max;
		}
		
		/// <summary>
		/// A bounded range for floats.
		/// </summary>
		/// <param name="min">The minimum acceptable value.</param>
		/// <param name="max">The maximum acceptable value.</param>
		/// <param name="decimals">How many decimals the inspector labels should display. Values must be in the [0,3]
		/// range. Default is 1.</param>
		public MinMaxRangeAttribute(float min, float max, uint decimals = 1)
		{
			Min = min;
			Max = max;
			Decimals = decimals;
		}

		#endregion
	}
}
