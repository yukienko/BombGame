using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
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
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
    }
}
