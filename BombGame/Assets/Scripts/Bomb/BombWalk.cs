using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using conv;

public class BombWalk : MonoBehaviour
{
    private const float bombWalkMaxSpeed = 0.02f;
    private const float bombWalkMinSpeed = 0.005f;
    private Vector3 bombWalkVector;
    private SpriteRenderer spriteRenderer;

    Vector3 bombRote = Vector3.zero;

    void Start()
    {
        var bombSpeedVectorX = Random.Range(bombWalkMinSpeed, bombWalkMaxSpeed);
        var bombSpeedVectorY = Random.Range(bombWalkMinSpeed, bombWalkMaxSpeed);
        var vectorRandX = ConvenientAssets.RandomBool();
        var vectorRandY = ConvenientAssets.RandomBool();
        bombWalkVector = new Vector3((vectorRandX ? bombSpeedVectorX : -bombSpeedVectorX), (vectorRandY ? bombSpeedVectorY : -bombSpeedVectorY), 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(bombWalkVector.x > 0)
        {
            FlipX();
        }
    }

    void Update()
    {
        //歩く処理(常に一定のスピードで向いているベクトルに進み続ける)
        transform.Translate(bombWalkVector);

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
        //縦の壁
        if(collision.transform.tag == "FieldWallV")
        {
            bombWalkVector.x *= -1;
            bombWalkVector.z = 0;
            FlipX();
            return;
        }
        //横の壁
        if(collision.transform.tag == "FieldWallH")
        {
            bombWalkVector.y *= -1;
            bombWalkVector.z = 0;
            return;
        }
    }

    void FlipX()
    {
        if (spriteRenderer.flipX == true)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }
}