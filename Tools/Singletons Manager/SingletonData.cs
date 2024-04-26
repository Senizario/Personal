using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class SingletonData
    {
        #region Variables & Properties

        [SerializeField] string type;
        public string _type
        {
            get => type;
        }
        static string defaultType = "None";
        public static string _defaultType
        {
            get => defaultType;
        }
        [SerializeField] GameObject gameObject;
        public GameObject _gameObject
        {
            get => gameObject;
            set => gameObject = value;
        }

        #endregion

        SingletonData() { }

        public SingletonData(string type, GameObject gameObject) 
        {
            this.type = type;

            this.gameObject = gameObject;
        }
    }
}
