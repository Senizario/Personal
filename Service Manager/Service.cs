using System;
using UnityEngine;

[Serializable]
public class Service
{
    #region Information

    [SerializeField] GameObject gameObject;
    public GameObject _gameObject
    {
        get => gameObject;
    }
    [SerializeField] Component component;
    public Component _component
    {
        get => component;
    }

    #endregion

    public Service(GameObject gameObject, Component component)
    {
        this.gameObject = gameObject;

        this.component = component;
    }
}
