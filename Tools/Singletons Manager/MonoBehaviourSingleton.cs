using UnityEngine;

namespace Utilities
{
    public class MonoBehaviourSingleton : MonoBehaviour
    {
        #region Variables & Properties

        [Header("Members & Properties")]
        [SerializeField] bool permanent;

        #endregion

        protected virtual void Awake()
        {
            SingletonsManager._instance.RegisterMonoBehaviourAsSingleton(this, permanent);
        }

        public virtual void OnRegister() { }
    }
}