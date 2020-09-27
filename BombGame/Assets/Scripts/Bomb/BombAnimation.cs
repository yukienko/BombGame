using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using conv;

public class BombAnimation : MonoBehaviour
{
    private new Rigidbody rigidbody = default;      //つかんで離したときに壁に埋まるのを防止
    private Animator bombAnimator = default;
    private bool isAnimated;

    //アニメーションで使う値
    private static Vector3 widthMax = new Vector3(6.0f, 3.5f, 0);
    private const float animeSpeed = 2.0f;
    private const float intervalSubRadius = 0.01f;
    private float radius;

    private float timeAnimeElapsed;
    private int catchPosValue;


    //計算した値を入れる
    private Vector3 bombMovePos;

    private ANIMATIONSTATE animationState;

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

    private enum ANIMATIONSTATE
    {
        None,
        StartAnime,
        PlayAnime,
        EndAnime,
    }

    private void Awake()
    {
        Init();
        isAnimated = false;
    }

    private void Init()
    {
        bombAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        radius = 2.0f;
        rigidbody.isKinematic = false;
    }

    private void SetState(ANIMATIONSTATE state)
    {
        switch (state)
        {
            case ANIMATIONSTATE.None:
                animationState = ANIMATIONSTATE.None;
                break;
            case ANIMATIONSTATE.StartAnime:
                animationState = ANIMATIONSTATE.StartAnime;
                break;
            case ANIMATIONSTATE.PlayAnime:
                animationState = ANIMATIONSTATE.PlayAnime;
                break;
            case ANIMATIONSTATE.EndAnime:
                animationState = ANIMATIONSTATE.EndAnime;
                break;
        }
    }

    private void Update()
    {
        switch (animationState)
        {
            case ANIMATIONSTATE.None:
                if (!isCatchBomb())
                {
                    if (CatchZoneDecision())
                    {
                        SetState(ANIMATIONSTATE.StartAnime);
                    }
                }
                break;
            //アニメ開始準備
            case ANIMATIONSTATE.StartAnime:
                Init();//初回用
                SetState(ANIMATIONSTATE.PlayAnime);
                break;
            //アニメーション
            case ANIMATIONSTATE.PlayAnime:
                Animation();
                break;
            case ANIMATIONSTATE.EndAnime:
                isAnimated = false;

                SetState(ANIMATIONSTATE.None);
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
        SetState(ANIMATIONSTATE.EndAnime);
        bombAnimator.SetBool("CatchAnim", false);
        GetComponent<BombAnimation>().enabled = false;
        gameObject.layer = 11;
    }

    bool CatchZoneDecision()
    {
        float distance = 100; // 飛ばす&表示するRayの長さ
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // !!!!!!!!!!!!!
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.tag == "CatchZone" && !isAnimated)
            {
                isAnimated = true;
                bombAnimator.SetBool("CatchAnim", true);
                SetState(ANIMATIONSTATE.StartAnime);
                rigidbody.isKinematic = true;

                if (hit.collider.transform.name == "CapZone1")
                    catchPosValue = 0;
                else if (hit.collider.transform.name == "CapZone2")
                    catchPosValue = 1;
                else if (hit.collider.transform.name == "CapZone3")
                    catchPosValue = 2;
                else if (hit.collider.transform.name == "CapZone4")
                    catchPosValue = 3;

                return true;
            }
        }
        return false;
    }

    bool isCatchBomb()
    {
        return (bombAnimator.GetBool("Catch"));
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "CatchZone" && !isAnimated)
    //    {
    //        isAnimated = true;
    //        bombAnimator.SetBool("CatchAnim", true);
    //        SetState(ANIMATIONSTATE.StartAnime);
    //        rigidbody.isKinematic = true;

    //        if (collision.transform.name == "CapZone1")
    //            catchPosValue = 0;
    //        else if (collision.transform.name == "CapZone2")
    //            catchPosValue = 1;
    //        else if (collision.transform.name == "CapZone3")
    //            catchPosValue = 2;
    //        else if (collision.transform.name == "CapZone4")
    //            catchPosValue = 3;
    //    }
    //}
}