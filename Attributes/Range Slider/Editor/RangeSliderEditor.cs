using UnityEditor;
using UnityEngine;

namespace Attributes
{
    [CustomPropertyDrawer(typeof(RangeSlider))]
    public class RangeSliderEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Vector2)
            {
				position = EditorGUI.PrefixLabel(position, label);

				float indentSpace = EditorGUI.indentLevel * 16f;

                float x = property.vector2Value.x;

                x = EditorGUI.FloatField(new Rect(-indentSpace + position.x, position.y, indentSpace + 48f, position.height), float.Parse(x.ToString("F2")));

                float y = property.vector2Value.y;

                y = EditorGUI.FloatField(new Rect(-indentSpace + position.x + position.width - 48f, position.y, indentSpace + 48f, position.height), float.Parse(y.ToString("F2")));

                EditorGUI.MinMaxSlider(new Rect(-indentSpace + position.x + 58f, position.y, indentSpace + position.width - 116f, position.height), ref x, ref y, ((RangeSlider)attribute).min, ((RangeSlider)attribute).max);

                property.vector2Value = new Vector2(x, y);
            }
            else
                EditorGUI.LabelField(position, label.text, "RangeSliderEditor/OnGUI/Range slider only works for Vector2");
        }
    }
}
