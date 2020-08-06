using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public ENEMYCOLOR enemyColor;
    public enum ENEMYCOLOR
    {
        red = 1,
        blue,
        yellow,
        green,
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        Init();
    }

    private void Init()
    {
        //初期化
        animator.SetInteger("Color", (int)enemyColor);
    }
}
