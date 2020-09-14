using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BombBase : MonoBehaviour
{
    private Animator animator = default;
    private const float InitPositionZ = 10f;
    private new Rigidbody rigidbody = default;

    public ENEMYCOLOR enemyColor;

    public enum ENEMYCOLOR
    {
        red = 1,
        blue,
        yellow,
        green,
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        CheckUsedStopTimeItem();
        CheckUsedAllDeleteItem();

        //CheckCatchedBomb();
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

    private void CheckUsedStopTimeItem()
    {
        if (ItemState.StopTimeItemState == ItemState.ITEMSTATE.isUse)
        {
            animator.speed = 0f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    private void CheckUsedAllDeleteItem()
    {
        if (ItemState.AllDeleteItemState == ItemState.ITEMSTATE.isUse)
        {
            Destroy(gameObject);
        }
    }

    private void CheckCatchedBomb()
    {
        if (animator.GetBool("Catch"))
        {
            //つかまれてるのでボムの物理演算をしない
            rigidbody.isKinematic = true;
        }
        else
        {
            rigidbody.isKinematic = false;
        }
    }
}
