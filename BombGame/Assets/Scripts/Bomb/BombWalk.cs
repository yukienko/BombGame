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
    public bool isCollisionStay;
    private string wallTag;
    private float timeSpeedUp;
    Vector3 bombRote = Vector3.zero;

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
        isCollisionStay = false;
        transform.GetComponent<Animator>().SetFloat("Speed"
            ,Mathf.Abs(bombWalkVector.x * bombWalkVector.y) * 10000);

        Debug.Log(transform.GetComponent<Animator>().GetFloat("Speed"));
    }

    void Update()
    {
        //歩く処理(常に一定のスピードで向いているベクトルに進み続ける)
        transform.Translate(bombWalkVector * 100 * Time.deltaTime);

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
        //if (!isCollisionStay)
        {
            isCollisionStay = true;
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
                Debug.LogError("!ありえん壁あるんやが？");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (wallTag == collision.transform.tag)
        {
            isCollisionStay = false;
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
}