using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T: class, new()
{
    private static T instance = new T();
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
}
