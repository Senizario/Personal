using Tools.ServicesManager;
using UnityEngine;

public static class ServicesManagerExtension
{
    public static void RegisterService<T>(this T component, bool permanent = false) where T : Component
    {
        if (ServicesManager._instance == null)
            ServicesManager._instance = Object.FindObjectOfType<ServicesManager>();

        ServicesManager._instance.Register(component, permanent);
    }

    public static T GetService<T>(this Component component) where T: Component
    {
        if (ServicesManager._instance == null)
            ServicesManager._instance = Object.FindObjectOfType<ServicesManager>();

        return ServicesManager._instance.Get<T>();
    }
}
