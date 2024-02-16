using Tools.SingletonsManager;

public static class SingletonsManagerExtension
{
    public static T GetSingleton<T>(this UnityEngine.MonoBehaviour monoBehaviour) where T: UnityEngine.MonoBehaviour
    {
        return SingletonsManager._instance.GetSingleton<T>();
    }
}
