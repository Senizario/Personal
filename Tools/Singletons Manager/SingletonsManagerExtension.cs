using UnityEngine;
using Utilities;

public static class SingletonsManagerExtension
{
    public static T GetSingleton<T>(this MonoBehaviour monoBehaviour) where T: MonoBehaviour
    {
        return SingletonsManager._instance.GetSingleton<T>();
    }
}
