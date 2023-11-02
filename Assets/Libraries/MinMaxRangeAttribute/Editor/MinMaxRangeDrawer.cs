using System;
using UnityEditor;
using UnityEngine;

namespace SneakySquirrelLabs.MinMaxRangeAttribute.Editor
{
	[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
	internal class MinMaxRangeDrawer : PropertyDrawer
	{
		#region Fields

		private const float HorizontalSpacing = 5f;
		private const float SliderHandlerWidth = 12f;
		
		private static readonly float VerticalSpacing = EditorGUIUtility.standardVerticalSpacing;
		private static GUIStyle LabelStyleField;
		
		private uint _decimals;

		#endregion

		#region Properties

		private static GUIStyle LabelStyle => LabelStyleField ??= new GUIStyle(GUI.skin.label);

		#endregion
		
		#region Update

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label) * 2;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (attribute is not MinMaxRangeAttribute minMaxAttribute)
			{
				Debug.LogError("Min max range attribute failed to draw.");
				return;
			}

			var minLimit = minMaxAttribute.MinLimit;
			var maxLimit = minMaxAttribute.MaxLimit;
			_decimals = minMaxAttribute.Decimals;
			
			if (property.propertyType == SerializedPropertyType.Vector2Int)
			{
				var value = property.vector2IntValue;
				var minValue = (float)value.x;
				var maxValue = (float)value.y;
				(minValue, maxValue) = ClampValues(minValue, maxValue, minLimit, maxLimit);
				DrawSlider(position, property, minLimit, maxLimit, ref minValue, ref maxValue, BuildIntLabel);
				value.x = (int)minValue;
				value.y = (int)maxValue;
				property.vector2IntValue = value;
			} 
			else if (property.propertyType == SerializedPropertyType.Vector2)
			{
				var value = property.vector2Value;
				var minValue = value.x;
				var maxValue = value.y;
				(minValue, maxValue) = ClampValues(minValue, maxValue, minLimit, maxLimit);
				DrawSlider(position, property, minLimit, maxLimit, ref minValue, ref maxValue, BuildFloatLabel);
				value.x = minValue;
				value.y = maxValue;
				property.vector2Value = value;
			}

			static (float, float) ClampValues(float minValue, float maxValue, float minLimit, float maxLimit)
			{
				minValue = Math.Max(minLimit, minValue);
				minValue = Math.Min(maxLimit, minValue);
				maxValue = Math.Min(maxLimit, maxValue);
				maxValue = Math.Max(maxValue, minValue);
				return (minValue, maxValue);
			}
			
			static void DrawSlider(Rect position, SerializedProperty property, float min, float max, ref float x, 
				ref float y, Func<float, GUIContent> buildLabel)
			{
				var consumedX = 0f;
				var firstLineRect = new Rect(position) { height = position.height / 2 - VerticalSpacing};
				
				// Field name
				consumedX += DrawFieldName(firstLineRect, property);
				
				// Min label
				var minLabel = buildLabel(min);
				consumedX += DrawLabel(firstLineRect, consumedX, minLabel);
				consumedX += HorizontalSpacing; // Add spacing between min label and slider
				
				// Slider
				var maxLabel = buildLabel(max);
				var maxLabelWidth = LabelStyle.CalcSize(maxLabel).x; // We need to know the max label's size
				var sliderWidth = firstLineRect.width - consumedX - maxLabelWidth - HorizontalSpacing;
				var sliderPosition = new Rect(firstLineRect) { x = firstLineRect.x + consumedX, width = sliderWidth };
				EditorGUI.MinMaxSlider(sliderPosition, ref x, ref y, min, max);
				consumedX += sliderWidth + HorizontalSpacing;
				
				// Max label
				DrawLabel(firstLineRect, consumedX, maxLabel);
				
				// Value labels
				var secondLineRect = new Rect(position) { y = position.y, height = firstLineRect.height};
				var valuesY = secondLineRect.y + sliderPosition.height + EditorGUIUtility.standardVerticalSpacing;
				// X label
				var labelsPosition = new Rect(sliderPosition) { y = valuesY };
				DrawValueLabel(labelsPosition, x, min, max, true, buildLabel);
				// Label
				DrawValueLabel(labelsPosition, y, min, max, false, buildLabel);
				
				static float DrawFieldName(Rect position, SerializedProperty property)
				{
					var labelPosition = new Rect(position) { width = EditorGUIUtility.labelWidth };
					EditorGUI.LabelField(labelPosition, property.displayName);
					return labelPosition.width;
				}

				static float DrawLabel(Rect position, float xOffset, GUIContent label)
				{
					var size = LabelStyle.CalcSize(label);
					var minLabelPosition = new Rect(position) { x = position.x + xOffset, width = size.x};
					EditorGUI.LabelField(minLabelPosition, label, LabelStyle);
					return size.x;
				}

				static void DrawValueLabel(Rect position, float value, float minLimit, float maxLimit, 
					bool applyExtraOffset, Func<float, GUIContent> buildLabel)
				{
					var label = buildLabel(value);
					var labelSize = LabelStyle.CalcSize(label);
					var relativePosition = (value - minLimit) / (maxLimit - minLimit);
					var offset = SliderHandlerWidth / 2 + (applyExtraOffset ? -labelSize.x : 0);
					var totalWidth = position.width - SliderHandlerWidth;
					var x = position.x + relativePosition * totalWidth + offset;
					var labelPosition = new Rect(position) { x = x, width = labelSize.x };
					EditorGUI.LabelField(labelPosition, label, LabelStyle);
				}
			}

			static GUIContent BuildIntLabel(float value) => new($"{value:F0}");

			GUIContent BuildFloatLabel(float value)
			{
				var floatLabel = _decimals switch
				{
					0 => $"{value:F0}",
					1 => $"{value:F1}",
					2 => $"{value:F2}",
					3 => $"{value:F3}",
					_ => throw new NotSupportedException("Min max attribute supports up to 3 decimal places.")
				};
				return new GUIContent(floatLabel);
			}
		}

		#endregion
	}
}