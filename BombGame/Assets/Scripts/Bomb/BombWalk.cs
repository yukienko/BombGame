using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWalk : MonoBehaviour
{
    private const float bombWalkSpeed = 0.02f;
    private Vector3 bombWalkVector;

    Vector3 bombRote = Vector3.zero;

    void Start()
    {
        var hoge = Random.Range(0.005f, bombWalkSpeed);
        bombWalkVector = new Vector3(Random.Range(-hoge, hoge), Random.Range(-hoge, hoge), 0);

        if(bombWalkVector.x < 0)
        {
            Vector3 turnToRight = new Vector3(0, 180, 0);
            bombRote = turnToRight;
        }
        else
        {
            Vector3 turnToLeft = new Vector3(0, 0, 0);
            bombRote = turnToLeft;
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
        transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hoge");
        //縦の壁
        if(collision.transform.tag == "FieldWallV")
        {
            bombWalkVector.x *= -1;
            bombWalkVector.z = 0;
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
}