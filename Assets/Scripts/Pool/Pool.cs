using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Object;

[Serializable]
public class Pool
{
    public string name;

    public Component component;

    public int size = 0;

    // TODO : Implement non-lazy loading
    // public bool lazyLoad = true;

    public Transform container;

    private readonly List<Component> pool = new();

    public void Init()
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(component.gameObject, container);
            obj.SetActive(false);
            pool.Add(obj.GetComponent(component.GetType()));
        }
    }

    public T Get<T>() where T : Component, IPoolObject
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                ActivateObject(pool[i] as T);
                return pool[i].GetComponent<T>();
            }
        }

        if (size != 0) throw new Exception("Pool is full");

        T component = CreateObject<T>();
        pool.Add(component);

        return component;
    }

    public void Return<T>(T component) where T : Component, IPoolObject
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].GetInstanceID() == component.GetInstanceID())
            {
                DeActivateObject(pool[i]);
                return;
            }
        }
    }

    private T CreateObject<T>() where T : Component, IPoolObject
    {
        GameObject obj = Instantiate(component.gameObject, container);
        T createdComponent = obj.GetComponent<T>();
        pool.Add(createdComponent);

        return createdComponent;
    }

    private void ActivateObject<T>(T component) where T : Component, IPoolObject
    {
        component.gameObject.SetActive(true);
        component.OnObjectActivated();
    }

    private void DeActivateObject(Component obj)
    {
        obj.gameObject.SetActive(false);
    }

    public static Pool Create<T>(GameObject prefab) where T : Component, IPoolObject
    {
        return new Pool
        {
            name = typeof(T).Name,
            component = prefab.GetComponent<T>(),
            size = 0,
        };
    }
}