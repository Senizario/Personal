using Attributes;
using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
    #region Singleton
    [Foldout("Singleton")]
    [SerializeField] protected bool permanent;
    #endregion

    protected virtual void Start()
    {
        ServicesManager.instance.Register(this as T, permanent);
    }
}
