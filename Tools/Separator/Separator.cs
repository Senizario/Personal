#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

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

