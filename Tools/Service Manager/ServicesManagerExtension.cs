using Tools.ServicesManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ServicesManagerExtension
{
    public static void RegisterService<T>(this T component, bool permanent = false) where T : Component
    {
        ServicesManager._instance.Clean(SceneManager.GetActiveScene());

        ServicesManager._instance.Register(component, permanent);
    }

    public static T GetService<T>(this Component component) where T: Component
    {
        ServicesManager._instance.Clean(SceneManager.GetActiveScene());

        return ServicesManager._instance.Get<T>();
    }
}
