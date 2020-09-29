using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T: class, new()
{
    public static T Instance
    {
        get
        {
            return new T();
        }
    }
}
