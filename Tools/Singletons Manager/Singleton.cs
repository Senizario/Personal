using System;
using UnityEngine;

namespace Tools.SingletonsManager
{
    [Serializable]
    public class Singleton
    {
        #region Fields & Properties

        [SerializeField] GameObject gameObject;
        public GameObject _gameObject
        {
            get => gameObject;
        }
        [SerializeField] UnityEngine.MonoBehaviour monoBehaviour;
        public UnityEngine.MonoBehaviour _monoBehaviour
        {
            get => monoBehaviour;
        }

        #endregion

        Singleton() { }

        public Singleton(GameObject gameObject, UnityEngine.MonoBehaviour monoBehaviour)
        {
            this.gameObject = gameObject;

            this.monoBehaviour = monoBehaviour;
        }
    }
}
