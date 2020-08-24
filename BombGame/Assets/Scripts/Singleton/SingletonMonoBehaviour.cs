using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    protected virtual bool dontDestroy { get; } = false;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance== null)
                {
                    GameObject singleton = new GameObject();
                    singleton.name = t.ToString() + "(singleton)";
                    instance = singleton.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (dontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }

        if (this != Instance)
        {
            Destroy(this);
            return;
        }
    }
}
