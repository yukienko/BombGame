using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using conv;
using StateManager;
using StaticManager;

public class BombAnimation : MonoBehaviour
{
    BombBase bombBase;

    private new Rigidbody rigidbody = default;      //つかんで離したときに壁に埋まるのを防止
    private Animator bombAnimator = default;
    private TouchManager _touchManager;
    private TouchManager old_phase;
    private bool isAnimated;

    //アニメーションで使う値
    private static Vector3 widthMax = new Vector3(6.0f, 3.5f, 0);
    private const float animeSpeed = 2.0f;
    private const float intervalSubRadius = 0.01f;
    private float radius;

    private bool isCatchZone;
    private float timeAnimeElapsed;
    private int catchPosValue;


    //計算した値を入れる
    private Vector3 bombMovePos;

    private E_ANIMATIONSTATE animationState;
    private E_BOMBCOLORFORPANEL bombColorForPanel;

    //番号振り分け
    //UIUIUIUIUIUIUIUIUIUIUIUIUIUI
    //UI                        UI
    //UI   1                2   UI
    //UI                        UI
    //UI                        UI
    //UI                        UI
    //UI                        UI
    //UI   4                3   UI
    //UI                        UI
    private Vector3[] CatchPos =
        {ConvenientAssets.v2Tov3(-widthMax.x,widthMax.y),
        ConvenientAssets.v2Tov3(widthMax.x,widthMax.y),
        ConvenientAssets.v2Tov3(widthMax.x,-widthMax.y),
        ConvenientAssets.v2Tov3(-widthMax.x,-widthMax.y)};

    private enum E_ANIMATIONSTATE
    {
        None,
        StartAnime,
        PlayAnime,
        BombAnime,
        EndAnime,
    }

    private enum E_BOMBCOLORFORPANEL
    {
        red = 1,
        blue,
        yellow,
        green,
    }

    private void Awake()
    {
        Init();
        isAnimated = false;
        _touchManager = new TouchManager();
        old_phase = new TouchManager();
    }

    private void Init()
    {
        bombBase = GetComponent<BombBase>();
        bombAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        radius = 2.0f;
        rigidbody.isKinematic = false;
        isCatchZone = false;
    }

    private void SetState(E_ANIMATIONSTATE state)
    {
        switch (state)
        {
            case E_ANIMATIONSTATE.None:
                animationState = E_ANIMATIONSTATE.None;
                break;
            case E_ANIMATIONSTATE.StartAnime:
                animationState = E_ANIMATIONSTATE.StartAnime;
                break;
            case E_ANIMATIONSTATE.PlayAnime:
                animationState = E_ANIMATIONSTATE.PlayAnime;
                break;
            case E_ANIMATIONSTATE.BombAnime:
                animationState = E_ANIMATIONSTATE.BombAnime;
                break;
            case E_ANIMATIONSTATE.EndAnime:
                animationState = E_ANIMATIONSTATE.EndAnime;
                break;
        }
    }

    private void Update()
    {
        TouchManagerUpdate();


        switch (animationState)
        {
            case E_ANIMATIONSTATE.None:
                if (!isAnimated)
                {
                    int hoge = CatchZoneDecision();
                    if (hoge == 0)
                    {
                        //ボムとパネルの色があってるのでアニメーションに移る
                        SetState(E_ANIMATIONSTATE.StartAnime);
                    }
                    else if(hoge == 1)
                    {
                        //なんもなし
                    }
                    else if(hoge == 2)
                    {
                        //爆発
                        SetState(E_ANIMATIONSTATE.BombAnime);
                        bombAnimator.SetBool("Explosion", true);
                    }
                }
                break;
            //アニメ開始準備
            case E_ANIMATIONSTATE.StartAnime:
                Init();//初回用
                SetState(E_ANIMATIONSTATE.PlayAnime);
                break;
            //アニメーション
            case E_ANIMATIONSTATE.PlayAnime:
                Animation();
                break;
            case E_ANIMATIONSTATE.BombAnime:
                break;
            case E_ANIMATIONSTATE.EndAnime:
                isAnimated = false;

                SetState(E_ANIMATIONSTATE.None);
                break;
        }
    }

    bool updateAnimeTimer(float timeOut)
    {
        timeAnimeElapsed += Time.deltaTime;
        if (timeAnimeElapsed >= timeOut)
        {
            timeAnimeElapsed = 0.0f;
            return true;
        }
        return false;
    }

    void Animation()
    {
        bombMovePos = new Vector3(
            radius * Mathf.Sin(Time.time * animeSpeed),
            radius * Mathf.Cos(Time.time * animeSpeed),
            10);

        transform.position = bombMovePos + CatchPos[catchPosValue];

        if (radius > 0)
        {
            if (updateAnimeTimer(0.01f))
            {
                radius -= intervalSubRadius;
            }
        }
        else
        {
            EndAnime();
        }
    }

    private void EndAnime()
    {
        SetState(E_ANIMATIONSTATE.EndAnime);
        bombAnimator.SetBool("CatchAnim", false);
        GetComponent<BombAnimation>().enabled = false;
        gameObject.layer = 11;
    }

    int CatchZoneDecision()
    {
        float distance = 100; // 飛ばす&表示するRayの長さ
        float duration = 3;   // 表示期間（秒）

        Ray ray = new Ray(transform.position, new Vector3(0, 0, 10));
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, duration, false);

        if (!bombAnimator.GetBool("Catch"))
        {
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, distance))
            {
                if (hit.collider.tag == "CatchZone" && !isAnimated)
                {
                    isAnimated = true;
                    bombAnimator.SetBool("CatchAnim", true);
                    rigidbody.isKinematic = true;

                    if (hit.collider.transform.name == "CapZone1" && (int)E_BOMBCOLORFORPANEL.blue == (int)bombBase.enemyColor)
                    {
                        catchPosValue = 0;
                        return 0;
                    }
                    else if (hit.collider.transform.name == "CapZone2" && (int)E_BOMBCOLORFORPANEL.green == (int)bombBase.enemyColor)
                    {
                        catchPosValue = 1;
                        return 0;
                    }
                    else if (hit.collider.transform.name == "CapZone3" && (int)E_BOMBCOLORFORPANEL.yellow == (int)bombBase.enemyColor)
                    {
                        catchPosValue = 2;
                        return 0;
                    }
                    else if (hit.collider.transform.name == "CapZone4" && (int)E_BOMBCOLORFORPANEL.red == (int)bombBase.enemyColor)
                    {
                        catchPosValue = 3;
                        return 0;
                    }
                    //キャッチゾーンに来たけど色とパネルの色があってなかった→爆発
                    return 2;
                }
            }
        }
        return 1;
    }

    Vector3 AlmightyTapPosition()
    {
        Vector3 vPoint = _touchManager._touch_position;
        return Application.isEditor ? Input.mousePosition : vPoint;
    }

    void TouchManagerUpdate()
    {
        _touchManager.update();
        _touchManager.GetTouch();
    }

    bool isCatchBomb()
    {
        return (bombAnimator.GetBool("Catch"));
    }
}