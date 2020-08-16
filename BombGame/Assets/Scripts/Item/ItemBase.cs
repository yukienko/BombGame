using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public Item itemName { get; set; }
    public enum Item
    {
        stopTime,
        barrier,
        decreaseEnemyLimit,
        allDelete
    }

    protected virtual ItemState.ITEMSTATE itemState { get; set; }
    protected virtual float itemUsingTime { get; set; } = 0;


    private void Start()
    {
        itemState = ItemState.ITEMSTATE.canUse; //テスト用：アイテム使用可能状態に変更
        Init();
    }

    private void Update()
    {
        
    }

    protected void Init()
    {
        if (itemState == ItemState.ITEMSTATE.canUse)
        {
            itemState = ItemState.ITEMSTATE.unUsed;
        }
        else
        {
            Finish();
        }
    }

    public virtual void UseItem()
    {
        if (itemState == ItemState.ITEMSTATE.unUsed)
        {
            itemState = ItemState.ITEMSTATE.isUse;
            Invoke("Finish", itemUsingTime);
        }
    }

    public virtual void Finish()
    {
        itemState = ItemState.ITEMSTATE.finish;
    }
}
