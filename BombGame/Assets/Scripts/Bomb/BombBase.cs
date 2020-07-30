using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    public ENEMYCOLOR enemyColor;
    public enum ENEMYCOLOR
    {
        red,
        blue,
        yellow,
        green
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //初期化
    }
}
