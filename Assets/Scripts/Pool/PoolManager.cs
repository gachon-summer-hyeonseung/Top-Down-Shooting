using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] Pool[] pools;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Pool pool in pools)
        {
            pool.Init();
        }
    }

    public T GetByType<T>() where T : Component, IPoolObject
    {
        foreach (Pool pool in pools)
        {
            if (pool.component.GetType() == typeof(T))
            {
                return pool.Get<T>();
            }
        }

        throw new System.Exception("Pool not found");
    }

    public T GetByType<T>(int index) where T : Component, IPoolObject
    {
        List<Pool> typedPools = pools.Where(pool => pool.component.GetType() == typeof(T)).ToList();
        return typedPools[index].Get<T>();
    }

    public void ReturnByType<T>(T component) where T : Component, IPoolObject
    {
        foreach (Pool pool in pools)
        {
            if (pool.component.GetType() == typeof(T))
            {
                pool.Return(component);
                return;
            }
        }

        throw new System.Exception("Pool not found");
    }

    public void ReturnByType<T>(string poolName, T component) where T : Component, IPoolObject
    {
        foreach (Pool pool in pools)
        {
            if (pool.component.GetType() == typeof(T) && pool.name == poolName)
            {
                pool.Return(component);
                return;
            }
        }

        throw new System.Exception("Pool not found");
    }
}
