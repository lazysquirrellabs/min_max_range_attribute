using System;
using UnityEngine;

namespace SneakySquirrelLabs.MinMaxRangeAttribute
{
	[AttributeUsage(AttributeTargets.Field)]
	public class MinMaxRangeAttribute : PropertyAttribute
	{
		#region Fields

		public float Min;
		public float Max;

		#endregion

		#region Setup

		public MinMaxRangeAttribute(int min, int max)
		{
			Min = min;
			Max = max;
		}
		
		public MinMaxRangeAttribute(float min, float max)
		{
			Min = min;
			Max = max;
		}

		#endregion
	}
}
