using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace SneakySquirrelLabs.MinMaxRangeAttribute
{
	[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
	internal class MinMaxRangeDrawer : PropertyDrawer
	{
		#region Fields

		private const float Spacing = 5f;

		#endregion
		
		#region Update

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var minMaxAttribute = attribute as MinMaxRangeAttribute;
			if (minMaxAttribute == null)
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
				var labelPosition = new Rect(position) { width = EditorGUIUtility.labelWidth };
				EditorGUI.LabelField(labelPosition, property.displayName);
				var minLabel = new GUIContent(((int)min).ToString());
				var maxLabel = new GUIContent(((int)max).ToString());
				var style = new GUIStyle(GUI.skin.label);
				var minLabelWidth = style.CalcSize(minLabel).x;
				var maxLabelWidth = style.CalcSize(maxLabel).x;
				var minLabelPosition = new Rect(position) { x = position.x + labelPosition.width, width = minLabelWidth};
				EditorGUI.LabelField(minLabelPosition, minLabel);
				var sliderWidth = position.width - labelPosition.width - minLabelWidth - maxLabelWidth - 2 * Spacing;
				var sliderPosition = new Rect(position) { x = minLabelPosition.xMax + Spacing, width = sliderWidth };
				EditorGUI.MinMaxSlider(sliderPosition, ref x, ref y, min, max);
				var maxLabelPosition = new Rect(position) { x = sliderPosition.xMax + Spacing, width = maxLabelWidth };
				EditorGUI.LabelField(maxLabelPosition, maxLabel);
				property.vector2IntValue = new Vector2Int((int)x, (int)y);
				var valuesY = position.y + sliderPosition.height + EditorGUIUtility.standardVerticalSpacing;
				style.alignment = TextAnchor.MiddleRight;
				sliderWidth -= 10;
				DrawValueLabel((int)x, min, max, sliderPosition.xMin, valuesY, sliderWidth, style, true);
				style.alignment = TextAnchor.MiddleLeft;
				DrawValueLabel((int)y, min, max, sliderPosition.xMin, valuesY, sliderWidth, style, false);

				static void DrawValueLabel(int value, float min, float max, float x, float y, float width, GUIStyle style, bool alignRight)
				{
					var label = new GUIContent(value.ToString());
					var labelSize = style.CalcSize(label);
					var padding = alignRight ? -labelSize.x/2 : 0;
					x += (value - min) / (max - min) * width + padding;
					var position = new Rect(x, y, labelSize.x, labelSize.y);
					EditorGUI.LabelField(position, label, style);
				}

			}
		}

		#endregion
	}
}