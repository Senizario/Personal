using Attributes;
using UnityEngine;

namespace Tools.ServicesManager
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
    {
        #region Information
        [Foldout("Information")]
        [SerializeField] protected RegisterOn registerOn;     
        [Foldout("Information")]
        [SerializeField] protected bool permanent;
        #endregion
        

        protected virtual void Awake()
        {
            if(registerOn == RegisterOn.awake)
                ServicesManager._instance.Register(this as T, permanent);
        }

        protected virtual void Start()
        {
            if (registerOn == RegisterOn.start)
                ServicesManager._instance.Register(this as T, permanent);
        }
    }
}
