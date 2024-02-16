using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Tools.SingletonsManager
{
    [CustomEditor(typeof(SingletonsManager), true)]
    public class SingletonsManagerEditor : Editor
    {
        #region Fields & Properties

        SerializedProperty singletonsData;
		ReorderableList singletonsDataList;

        #endregion

        void OnEnable()
        {
            singletonsData = serializedObject.FindProperty("singletons");

			singletonsDataList = new ReorderableList(serializedObject, singletonsData, true, false, true, true)
            {
                drawElementCallback = (Rect position, int i, bool isActive, bool isFocused) =>
                {
                    EditorGUI.LabelField(new Rect(position.x, position.y, 54f, position.height), "Singletons");

                    SerializedProperty singletonData = singletonsData.GetArrayElementAtIndex(i);

                    SerializedProperty gameObject = singletonData.FindPropertyRelative("gameObject");

					EditorGUI.PropertyField(new Rect(position.x + 60f, position.y + 3f, (position.width - 60f) / 2f - 2f, position.height), gameObject, GUIContent.none, true);

                    if (gameObject.objectReferenceValue != null)
                    {
                        List<UnityEngine.MonoBehaviour> monoBehaviours = new List<UnityEngine.MonoBehaviour>(((GameObject)gameObject.objectReferenceValue).GetComponents<UnityEngine.MonoBehaviour>());

                        monoBehaviours.Insert(0, null);

                        int monoBehaviourIndex = 0;

                        for (int j = 0; j < monoBehaviours.Count; j++)
                        {
                            SerializedProperty monoBehaviour = singletonData.FindPropertyRelative("monoBehaviour");

							if ((UnityEngine.MonoBehaviour)monoBehaviour.objectReferenceValue == monoBehaviours[j])
                            {
                                monoBehaviourIndex = j;

                                break;
                            }
                        }

                        monoBehaviourIndex = EditorGUI.Popup(new Rect(position.x + 60f + ((position.width - 60f) / 2f) + 4f, position.y + 3f, position.width - (60f + ((position.width - 60f) / 2f) + 4f), position.height), monoBehaviourIndex, Array.ConvertAll(monoBehaviours.ToArray(), (UnityEngine.MonoBehaviour monoBehaviour) =>
                        {
                            if (monoBehaviour == null)
                                return "None";
                            else
                                return monoBehaviour.GetType().FullName;
                        }));

						singletonData.FindPropertyRelative("monoBehaviour").objectReferenceValue = monoBehaviours[monoBehaviourIndex];
                    }
                },
			};
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));

            GUI.enabled = true;

            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing * 3f);

            SerializedProperty singletons = serializedObject.FindProperty("singletons");

			singletons.isExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(singletons.isExpanded, "Singletons");

			EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

			if (singletons.isExpanded)
                this.singletonsDataList.DoLayoutList();

            EditorGUILayout.EndFoldoutHeaderGroup();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
