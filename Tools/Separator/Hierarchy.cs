#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Tools.Separator
{
    [InitializeOnLoad]
    public class Hierarchy
    {
		#region Fields & Properties

		#region Separator
        static Color color;
        #endregion

        #endregion

        static Hierarchy()
        {
            color = new Color(115f / 255f, 115f / 255f, 115f / 255f, 1f);
            
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

            SerializedProperty tags = tagManager.FindProperty("tags");

            bool founded = false;

            for (int i = 0; i < tags.arraySize; i++)
            {
                SerializedProperty tag = tags.GetArrayElementAtIndex(i);

                if (tag.stringValue.Equals("Separator"))
                {
                    founded = true;

                    break;
                }
            }

            if (!founded)
            {
                tags.InsertArrayElementAtIndex(0);

                SerializedProperty tag = tags.GetArrayElementAtIndex(0);

                tag.stringValue = "Separator";

                tagManager.ApplyModifiedProperties();
            }

            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        static void HierarchyWindowItemOnGUI(int instanceID, Rect position)
        {
            Object instanceObject = EditorUtility.InstanceIDToObject(instanceID);

            if (instanceObject != null
                &&
                instanceObject is GameObject)
            {
                if (((GameObject)instanceObject).CompareTag("Separator"))
                {
					EditorGUI.DrawRect(new Rect(position.x, position.y, 15f, 15f), color);

                    EditorGUI.DrawRect(new Rect(position.x, position.y + 15f, position.width, 1f), color);
                }
            }
        }
    }
}

#endif
