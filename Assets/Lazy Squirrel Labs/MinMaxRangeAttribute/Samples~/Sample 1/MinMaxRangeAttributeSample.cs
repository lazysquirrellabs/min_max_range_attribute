using UnityEngine;

namespace LazySquirrelLabs.MinMaxRangeAttribute.Sample1
{
	internal class MinMaxRangeAttributeSample : MonoBehaviour
	{
		// Vector 2 with integer values within the [1, 11] range.
		[SerializeField, MinMaxRange(1, 11)] private Vector2Int _rewardRange;
		// Vector 2 with integer values within the [0, 100] range.
		[SerializeField, MinMaxRange(0, 100)] private Vector2Int _allowedLevels;
		// Vector 2 with floating point values within the [0, 10] range, with default decimals (1).
		[SerializeField, MinMaxRange(0f, 10f)] private Vector2 _optimalSpeed;
		// Vector 2 with floating point values within the [0, 1] range, with 2 decimals.
		[SerializeField, MinMaxRange(0f, 1f, 2)] private Vector2 _normalTemperature;
		// Vector 2 with floating point values within the [0, 1] range, with 3 decimals.
		[SerializeField, MinMaxRange(0f, 1f, 3)] private Vector2 _relativeDistance;
	}
}