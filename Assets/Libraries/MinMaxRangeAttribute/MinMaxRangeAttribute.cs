using System;
using UnityEngine;

namespace SneakySquirrelLabs.MinMaxRangeAttribute
{
	[AttributeUsage(AttributeTargets.Field)]
	public class MinMaxRangeAttribute : PropertyAttribute
	{
		#region Fields

		public readonly float Min;
		public readonly float Max;
		public readonly uint Decimals;

		#endregion

		#region Setup

		public MinMaxRangeAttribute(int min, int max)
		{
			Min = min;
			Max = max;
		}
		
		public MinMaxRangeAttribute(float min, float max, uint decimals = 1)
		{
			Min = min;
			Max = max;
			Decimals = decimals;
		}

		#endregion
	}
}
