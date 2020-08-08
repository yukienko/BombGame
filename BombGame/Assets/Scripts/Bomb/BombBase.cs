﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BombBase : MonoBehaviour
{
    private Animator animator = default;
    private const float InitPositionZ = 10f;

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
        Init();
    }

    private void Init()
    {
        //初期化
        var initPos = gameObject.transform.position;
        initPos.z = InitPositionZ;
        gameObject.transform.position = initPos;

        animator = GetComponent<Animator>();
        animator.SetInteger("Color", (int)enemyColor);
    }

    public void Catch(bool isMove)
    {
        animator.SetBool("Catch", isMove);
    }
}