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
        InitItem();
    }

    private void Update()
    {
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

    private void InitItem()
    {
        CheckUsingStopTimeItem();
        CheckUsingSelectColorMoveItem();
        EventManager.StartListening(EventManager.Events.StopTimeItem, CheckUsingStopTimeItem);
        EventManager.StartListening(EventManager.Events.SelectColorMoveItem, CheckUsingSelectColorMoveItem);
    }

    public void Catch(bool isMove)
    {
        animator.SetBool("Catch", isMove);
    }

    private void CheckUsingStopTimeItem()
    {
        if (StopTimeItem.Instance.IsUsingItem())
        {
            animator.speed = 0f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    private void CheckUsingSelectColorMoveItem()
    {
        if (!SelectColorMoveItem.Instance.IsUsingItem())
        {
            return;
        }

        if (SelectColorMoveItem.Instance.IsUsingSelectColorItem(enemyColor))
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
