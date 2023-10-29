using UnityEditor;
using UnityEngine;

namespace SneakySquirrelLabs.MinMaxRangeAttribute
{
	[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
	internal class MinMaxRangeDrawer : PropertyDrawer
	{
		#region Fields

		private const float Spacing = 5f;
		private const float OffsetX = -17f;
		private const float OffsetY = 3f;

		#endregion
		
		#region Update

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
				var x = (float) value.x;
				var y = (float) value.y;
				var consumedX = 0f;
				
				// Field name
				consumedX += DrawFieldName(position, property);
				
				// Min label
				var style = new GUIStyle(GUI.skin.label);
				var minLabel = new GUIContent($"{min:F0}");
				consumedX += DrawLabel(position, consumedX, minLabel, style);
				consumedX += Spacing; // Add spacing between min label and slider
				
				// Slider
				var maxLabel = new GUIContent($"{max:F0}");
				var maxLabelWidth = style.CalcSize(maxLabel).x; // We need to know the max label's size
				var sliderWidth = position.width - consumedX - maxLabelWidth - Spacing;
				var sliderPosition = new Rect(position) { x = position.x + consumedX, width = sliderWidth };
				EditorGUI.MinMaxSlider(sliderPosition, ref x, ref y, min, max);
				consumedX += sliderWidth + Spacing;
				
				// Save value
				value.x = (int)x;
				value.y = (int)y;
				property.vector2IntValue = value;
				
				// Max label
				DrawLabel(position, consumedX, maxLabel, style);
				
				// Value labels
				var valuesY = position.y + sliderPosition.height + EditorGUIUtility.standardVerticalSpacing;
				style.alignment = TextAnchor.MiddleRight;
				// X label
				var labelsPosition = new Rect(sliderPosition) { y = valuesY };
				DrawValueLabel(labelsPosition, value.x, min, max, style, OffsetX);
				// Label
				style.alignment = TextAnchor.MiddleLeft;
				DrawValueLabel(labelsPosition, value.y, min, max, style, OffsetY);
			}

			static float DrawFieldName(Rect position, SerializedProperty property)
			{
				var labelPosition = new Rect(position) { width = EditorGUIUtility.labelWidth };
				EditorGUI.LabelField(labelPosition, property.displayName);
				return labelPosition.width;
			}

			static float DrawLabel(Rect position, float xOffset, GUIContent label, GUIStyle style)
			{
				var size = style.CalcSize(label);
				var minLabelPosition = new Rect(position) { x = position.x + xOffset, width = size.x};
				EditorGUI.LabelField(minLabelPosition, label, style);
				return size.x;
			}
			
			static void DrawValueLabel(Rect position, int value, float min, float max, GUIStyle style, float offset)
			{
				var label = new GUIContent(value.ToString());
				var labelSize = style.CalcSize(label);
				var relativePosition = (value - min) / (max - min);
				var x = position.x + relativePosition * position.width + offset;
				var labelPosition = new Rect(position) { x = x, width = labelSize.x};
				EditorGUI.LabelField(labelPosition, label, style);
			}
		}

		#endregion
	}
}