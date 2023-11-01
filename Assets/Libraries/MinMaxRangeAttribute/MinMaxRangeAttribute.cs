using System;
using UnityEngine;

namespace SneakySquirrelLabs.MinMaxRangeAttribute
{
	/// <summary>
	/// An attribute that simplifies defining bounded ranges (ranges with minimum and maximum limits) on the inspector.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class MinMaxRangeAttribute : PropertyAttribute
	{
		#region Fields

		public readonly float MinLimit;
		public readonly float MaxLimit;
		public readonly uint Decimals;

		#endregion

		#region Setup

		/// <summary>
		/// A bounded range for integers.
		/// </summary>
		/// <param name="minLimit">The minimum acceptable value.</param>
		/// <param name="maxLimit">The maximum acceptable value.</param>
		public MinMaxRangeAttribute(int minLimit, int maxLimit)
		{
			MinLimit = minLimit;
			MaxLimit = maxLimit;
		}
		
		/// <summary>
		/// A bounded range for floats.
		/// </summary>
		/// <param name="minLimit">The minimum acceptable value.</param>
		/// <param name="maxLimit">The maximum acceptable value.</param>
		/// <param name="decimals">How many decimals the inspector labels should display. Values must be in the [0,3]
		/// range. Default is 1.</param>
		public MinMaxRangeAttribute(float minLimit, float maxLimit, uint decimals = 1)
		{
			MinLimit = minLimit;
			MaxLimit = maxLimit;
			Decimals = decimals;
		}

		#endregion
	}
}
