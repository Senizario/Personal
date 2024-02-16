using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools.SingletonsManager
{
    public class SingletonsManager : MonoBehaviour
    {
        #region Singleton

        static SingletonsManager instance;
        public static SingletonsManager _instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SingletonsManager>();

                    if (instance == null)
                        instance = new GameObject("Singletons Manager", typeof(SingletonsManager)).GetComponent<SingletonsManager>();    
                    else
                    {
                        if (instance.gameObject.transform.parent != null)
                            instance.gameObject.transform.SetParent(null, true);
                    }

					instance.RegisterMonoBehaviours();

					DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
            set
            {
                if (instance == null)
                {
                    instance = value;

                    if (instance.gameObject.transform.parent != null)
                        instance.gameObject.transform.SetParent(null, true);

                    DontDestroyOnLoad(instance.gameObject);
                }
                else if (value != instance)
                    Destroy(value.gameObject);
            }
        }

        #endregion

        #region Fields & Properties

        [SerializeField] List<Singleton> singletons;
		Dictionary<Type, Singleton> _singletons { get; set; }
		bool monoBehavioursAreRegisted;

		#endregion

		void Awake()
        {
            if (!monoBehavioursAreRegisted)
                RegisterMonoBehaviours();

            _instance = this;
        }

        void OnEnable()
        {
            SceneManager.sceneUnloaded += Clean;
        }

        void OnDisable()
        {
            SceneManager.sceneUnloaded -= Clean;
        }

		public void RegisterMonoBehaviours()
		{
			_singletons = new Dictionary<Type, Singleton>();

            if (singletons == null)
                singletons = new List<Singleton>();

			for (int i = 0; i < singletons.Count; i++)
			{
				if (singletons[i]._gameObject != null)
				{
					if (singletons[i]._monoBehaviour != null)
					{
						Type type = singletons[i]._monoBehaviour.GetType();

                        if (!_singletons.ContainsKey(type))
                        {
                            _singletons.Add(type, singletons[i]);

                            if (singletons[i]._monoBehaviour is MonoBehaviourSingleton monoBehaviourSingleton)
                                monoBehaviourSingleton.OnRegister();
						}
					}
					else
						Debug.LogWarning($"SingletonsManager/RegisterMonoBehaviours/Singletons[{i}]._monoBehaviour = null");
				}
				else
					Debug.LogWarning($"SingletonsManager/RegisterMonoBehaviours/Singletons[{i}]._gameObject = null");
			}

			monoBehavioursAreRegisted = true;
		}

		public void RegisterMonoBehaviourAsSingleton<T>(T monoBehaviour, bool permanent = false) where T : UnityEngine.MonoBehaviour
        {
            Type type = monoBehaviour.GetType();

			if (!_singletons.ContainsKey(type))
            {
                Singleton singleton = new Singleton(monoBehaviour.gameObject, monoBehaviour);

                singletons.Add(singleton);

                _singletons.Add(type, singleton);

				if (singleton._monoBehaviour is MonoBehaviourSingleton monoBehaviourSingleton)
					monoBehaviourSingleton.OnRegister();

				if (permanent)
                    singleton._gameObject.transform.SetParent(gameObject.transform);
            }
		}

        public T GetSingleton<T>() where T : UnityEngine.MonoBehaviour
        {
            Type type = typeof(T);

			if (_singletons.TryGetValue(type, out Singleton singleton))
                return (T)singleton._monoBehaviour;
            else
            {
                T[] monoBehavious = FindObjectsByType<T>(FindObjectsSortMode.None);

                if (monoBehavious.Length == 0)
                {
                    Debug.LogWarning($"SingletonsManager/GetSingleton<{type}>/No MonoBehaviour whit this type was found in the scene");

                    return null;
                }

                if (monoBehavious.Length > 1)
                {
                    Debug.LogWarning($"SingletonsManager/GetSingleton<{type}>/More than one MonoBehaviour whit this type was found in the scene");

                    return null;
                }

                RegisterMonoBehaviourAsSingleton(monoBehavious[0], (monoBehavious[0] is MonoBehaviourSingleton monoBehaviourSingleton) ? monoBehaviourSingleton._permanent: false);

                return monoBehavious[0];
            }
        }

        public void Clean(Scene scene)
        {
            for (int i = 0; i < singletons.Count; i++)
            {
                if (!singletons[i]._monoBehaviour)
                {
                    singletons.RemoveAt(i);

                    i--;
                }
            }

            List<Type> nulledTypes = new List<Type>();

            foreach (KeyValuePair<Type, Singleton> singleton in _singletons)
            {
                if (!singleton.Value._monoBehaviour)
                    nulledTypes.Add(singleton.Key);
            }

            for (int i = 0; i < nulledTypes.Count; i++)
                _singletons.Remove(nulledTypes[i]);
        }
    }
}
