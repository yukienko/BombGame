using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnimation : MonoBehaviour
{
    private new Rigidbody rigidbody = default;      //つかんで離したときに壁に埋まるのを防止
    private bool isAnimated;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        isAnimated = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "CatchZone" && !isAnimated)
        {
            rigidbody.isKinematic = true;
            //ここでアニメーション

        }
    }

}
