using System;
using UnityEditor;
using UnityEngine;

namespace SneakySquirrelLabs.MinMaxRangeAttribute
{
	[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
	internal class MinMaxRangeDrawer : PropertyDrawer
	{
		#region Fields

		private const float HorizontalSpacing = 5f;
		private const float OffsetX = -17f;
		private const float OffsetY = 3f;
		private static readonly float VerticalSpacing = EditorGUIUtility.standardVerticalSpacing;

		private static GUIStyle LabelStyleField;

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

			var min = minMaxAttribute.Min;
			var max = minMaxAttribute.Max;
			
			if (property.propertyType == SerializedPropertyType.Vector2Int)
			{
				var value = property.vector2IntValue;
				var x = (float)value.x;
				var y = (float)value.y;
				DrawSlider(position, property, min, max, ref x, ref y, BuildIntLabel);
				value.x = (int)x;
				value.y = (int)y;
				property.vector2IntValue = value;
			} 
			else if (property.propertyType == SerializedPropertyType.Vector2)
			{
				var value = property.vector2Value;
				var x = value.x;
				var y = value.y;
				DrawSlider(position, property, min, max, ref x, ref y, BuildFloatLabel);
				value.x = x;
				value.y = y;
				property.vector2Value = value;
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
				// consumedX += DrawSlider(position, maxLabelWidth, min, max, ref x, ref y);
				consumedX += sliderWidth + HorizontalSpacing;
				
				// Max label
				DrawLabel(firstLineRect, consumedX, maxLabel);
				
				// Value labels
				var secondLineRect = new Rect(position) { y = position.y, height = firstLineRect.height};
				var valuesY = secondLineRect.y + sliderPosition.height + EditorGUIUtility.standardVerticalSpacing;
				LabelStyle.alignment = TextAnchor.MiddleRight;
				// X label
				var labelsPosition = new Rect(sliderPosition) { y = valuesY };
				DrawValueLabel(labelsPosition, x, min, max, OffsetX, buildLabel);
				// Label
				LabelStyle.alignment = TextAnchor.MiddleLeft;
				DrawValueLabel(labelsPosition, y, min, max, OffsetY, buildLabel);
				
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

				static void DrawValueLabel(Rect position, float value, float min, float max,
					float offset, Func<float, GUIContent> buildLabel)
				{
					var label = buildLabel(value);
					var labelSize = LabelStyle.CalcSize(label);
					var relativePosition = (value - min) / (max - min);
					var x = position.x + relativePosition * position.width + offset;
					var labelPosition = new Rect(position) { x = x, width = labelSize.x };
					EditorGUI.LabelField(labelPosition, label, LabelStyle);
				}
			}

			static GUIContent BuildIntLabel(float value) => new($"{value:F0}");
			
			static GUIContent BuildFloatLabel(float value) => new($"{value:F1}");
		}

		#endregion
	}
}