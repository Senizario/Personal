using UnityEngine;

namespace Tools.SingletonsManager
{
    public class MonoBehaviourSingleton : MonoBehaviour
    {
        #region Fields & Properties

        [Header("Fields & Properties (MonoBehaviourSingleton)")]
        [SerializeField] bool permanent;
        public bool _permanent
        {
            get => permanent;
        }

        #endregion

        protected virtual void Awake()
        {
            SingletonsManager._instance.RegisterMonoBehaviourAsSingleton(this, permanent);
        }

        public virtual void OnRegister() { }
    }
}