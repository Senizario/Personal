using UnityEditor;
using UnityEngine;

namespace Attributes
{
	[CustomPropertyDrawer(typeof(Prefix))]
	public class PrefixEditor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property, new GUIContent($"{((Prefix)attribute).prefix} {label.text}"));
		}
	}
}
