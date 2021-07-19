#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Tools.Separator
{
    [InitializeOnLoad]
    public class Hierarchy
    {
        #region Information

        #region Separator
        static Texture2D separatorIcon;
        static Color separatorColor;
        #endregion

        #endregion

        static Hierarchy()
        {
            /// Separator Tag
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
            ///

            /// Separator Icon
            separatorIcon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Tools/Separator/Icon.png", typeof(Texture2D));

            separatorColor = new Color(115f / 255f, 115f / 255f, 115f / 255f, 1f);
            ///

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
                    GUI.Label(new Rect(position.x - 2f, position.y, position.width, position.height), separatorIcon);

                    EditorGUI.DrawRect(new Rect(position.x, position.y + 15f, position.width, 1f), separatorColor);
                }
            }
        }
    }
}
#endif
