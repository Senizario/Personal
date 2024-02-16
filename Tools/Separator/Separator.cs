#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Tools.Separator
{
    public static class Separator
    {
        [MenuItem("GameObject/Separator", false, -1)]
        public static void Create()
        {
            GameObject separator = new GameObject("Separator")
            {
                tag = "Separator",
            };

            separator.transform.hideFlags = HideFlags.NotEditable;
        }
    }
}

#endif
