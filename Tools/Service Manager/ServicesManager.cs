using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools.ServicesManager
{
    public class ServicesManager : MonoBehaviour
    {
        #region Singleton
        static ServicesManager instance;
        public static ServicesManager _Instance
        {
            get => instance;
            set
            {
                if (instance == null)
                {
                    instance = value;

                    if (instance.gameObject.transform.parent != null)
                        instance.gameObject.transform.SetParent(null, true);

                    DontDestroyOnLoad(instance.gameObject);
                }
                else if (instance != value)
                    Destroy(value.gameObject);
            }
        }
        #endregion

        #region Information

        #if UNITY_EDITOR
        [SerializeField] bool isExpanded;
        #endif

        [SerializeField] List<Service> services;
        Dictionary<Type, object> _services { get; set; }

        #endregion

        void Awake()
        {
            if (_services == null)
                _services = new Dictionary<Type, object>();

            for (int i = 0; i < services.Count; i++)
            {
                if (services[i]._gameObject != null)
                {
                    if (services[i]._component != null)
                    {
                        Type type = services[i]._component.GetType();

                        if (!_services.ContainsKey(type))
                            _services.Add(type, services[i]._component);
                    }
                    else
                        Debug.Log($"{services[i]._gameObject.name} has not type");
                }
                else
                    Debug.Log($"GameObject {i} is empty");
            }

            _Instance = this;
        }

        void OnEnable()
        {
            SceneManager.sceneUnloaded += SceneUnloaded;
        }

        void OnDisable()
        {
            SceneManager.sceneUnloaded -= SceneUnloaded;
        }

        public void Register<T>(T service, bool permanent = false) where T : Component
        {
            if (_services == null)
                _services = new Dictionary<Type, object>();

            Type type = typeof(T);

            if (_services.ContainsKey(type))
                Debug.Log($"You can't add {typeof(T)} twice");
            else
            {
                services.Add(new Service(service.gameObject, service));

                if (permanent)
                {
                    Transform parent = service.gameObject.transform.parent;

                    if (parent == null)
                        service.gameObject.transform.SetParent(gameObject.transform);

                    while (parent != null)
                    {
                        if (parent.parent == null)
                        {
                            parent.SetParent(gameObject.transform);

                            break;
                        }
                        else
                            parent = parent.parent;
                    }
                }

                _services.Add(type, service);
            }
        }

        public T Get<T>() where T : Component
        {
            if (_services == null)
                _services = new Dictionary<Type, object>();

            if (_services.TryGetValue(typeof(T), out object service))
                return (T)service;
            else
            {
                if (_services.TryGetValue(typeof(T).BaseType, out object baseService))
                    return (T)baseService;
                else
                    return default;
            }
        }

        void SceneUnloaded(Scene scene)
        {
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i]._gameObject == null
                    ||
                    services[i]._component == null)
                {
                    if (_services.Remove(services[i]._type))
                    {
                        services.RemoveAt(i);

                        i--;
                    }
                    else if (_services.Remove(services[i]._type.BaseType))
                    {
                        services.RemoveAt(i);

                        i--;
                    }
                }
            }
        }
    }
}
