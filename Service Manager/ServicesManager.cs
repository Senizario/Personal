using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServicesManager : MonoBehaviour
{
    #region Singleton
    public static ServicesManager instance;
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
        _services = new Dictionary<Type, object>();

        for (int i = 0; i < services.Count; i++)
        {
            if (services[i]._gameObject != null)
            {
                if (services[i]._component != null)
                {
                    Type type = services[i]._component.GetType();

                    if (_services.ContainsKey(type))
                        Debug.Log($"You can't add {type} twice");
                    else
                        _services.Add(type, services[i]._component);
                }
                else
                    Debug.Log($"{services[i]._gameObject.name} has not type");
            }
            else
                Debug.Log($"GameObject {i} is empty");
        }

        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    void OnDisable()
    {
        SceneManager.sceneUnloaded += SceneUnloaded;
    }

    public void Register<T>(T service, bool permanent = false) where T : Component
    {
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
        if (_services.TryGetValue(typeof(T), out object service))
            return (T)service;

        return default;
    }

    void SceneUnloaded(Scene scene)
    {
        for (int i = 0; i < services.Count; i++)
        {
            if (services[i]._gameObject == null)
            {
                _services.Remove(services[i]._component.GetType());

                services.RemoveAt(i);

                i--;
            }
        }
    }
}
