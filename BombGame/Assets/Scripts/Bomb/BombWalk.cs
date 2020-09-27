using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using conv;
using UnityEngine.Assertions.Must;

public class BombWalk : MonoBehaviour
{
    private const float bombWalkMaxSpeed = 0.02f;               //ボムのあるくベクトルの大きさの最大値
    private const float bombWalkMinSpeed = 0.005f;              //ボムのあるくベクトルの大きさの最小値
    private const float revisionBombWalkSpeedValue = 100.0f;    //ボムの歩き速度調整用
    private Vector3 bombWalkVector;                             //ボムの進むベクトル
    private SpriteRenderer spriteRenderer;                      //画像反転用
    private string wallTag;                                     //壁認識用
    private float timeSpeedUp;                                  //時間がたつにつれ値を上げていく
    private Vector3 bombRote = Vector3.zero;                    //ボム回転の補正用
    private Animator bombAnimator;                              //つかんでるとき動作させないため


    private void Awake()
    {
        bombAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        timeSpeedUp = 1;
        var bombSpeedVectorX = Random.Range(bombWalkMinSpeed, bombWalkMaxSpeed) * timeSpeedUp;
        var bombSpeedVectorY = Random.Range(bombWalkMinSpeed, bombWalkMaxSpeed) * timeSpeedUp;
        var vectorRandX = ConvenientAssets.RandomBool();
        var vectorRandY = ConvenientAssets.RandomBool();
        bombWalkVector = new Vector3
            ((vectorRandX ? bombSpeedVectorX : -bombSpeedVectorX),
            (vectorRandY ? bombSpeedVectorY : -bombSpeedVectorY),
            0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(bombWalkVector.x > 0)
        {
            FlipX();
        }
        transform.GetComponent<Animator>().SetFloat("Speed"
            ,Mathf.Abs(bombWalkVector.x * bombWalkVector.y) * Mathf.Pow(revisionBombWalkSpeedValue,2));

        Debug.Log(transform.GetComponent<Animator>().GetFloat("Speed"));
    }

    void Update()
    {
        MoveUpdate();
    }

    void MoveUpdate()
    {
        //つかまれていないとき
        if (!isCatchBomb() && !isAnimeBomb())
        {
            //歩く処理(常に一定のスピードで向いているベクトルに進み続ける)
            transform.Translate(bombWalkVector * revisionBombWalkSpeedValue * Time.deltaTime);
            Debug.Log(bombWalkVector.x + "," + bombWalkVector.y);
        }
        //バグ修正までの補正
        fixrote();
    }

    void fixrote()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, 10);
        transform.rotation = Quaternion.Euler(bombRote);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isCatchBomb() && !isAnimeBomb())
        {
            //縦の壁
            if (collision.transform.tag == "FieldWallV")
            {
                wallTag = collision.transform.tag;
                bombWalkVector.x *= -1;
                bombWalkVector.z = 0;
                FlipX();
                return;
            }
            //横の壁
            else if (collision.transform.tag == "FieldWallH")
            {
                wallTag = collision.transform.tag;
                bombWalkVector.y *= -1;
                bombWalkVector.z = 0;
                return;
            }
            else
            {
                Debug.LogError("!ありえんコライダあるんやが？:" + collision.gameObject.name);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (wallTag == collision.transform.tag)
        {
            wallTag = null;
        }
    }

    void FlipX()
    {
        if (spriteRenderer.flipX == true)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    bool isCatchBomb()
    {
        return (bombAnimator.GetBool("Catch"));
    }
    bool isAnimeBomb()
    {
        return (bombAnimator.GetBool("CatchAnim"));
    }
}