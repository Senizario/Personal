using UnityEditor;
using UnityEngine;

namespace Attributes
{
	[CustomPropertyDrawer(typeof(Name))]
	public class NameEditor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property, new GUIContent(((Name)attribute).name));
		}
	}
}
