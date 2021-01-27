using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemBase<t> : Singleton<t> where t : class, new()
{
    public enum ITEMSTATE
    {
        CanUse,
        UnUsed,
        Using,
        Finish,
    }
    protected virtual ITEMSTATE ItemState { get; set; }
    protected virtual float ItemUsingTime { get; set; } = 0;

    protected BombBase.ENEMYCOLOR usingItemColor = default;

    public void Init()
    {
        ItemState = ITEMSTATE.CanUse; //テスト用：アイテム使用可能状態に変更

        if (ItemState == ITEMSTATE.CanUse)
        {
            ItemState = ITEMSTATE.UnUsed;
        }
        else
        {
            Finish();
        }
    }

    public virtual void UseItem()
    {
        if (ItemState == ITEMSTATE.UnUsed)
        {
            ItemState = ITEMSTATE.Using;
            DOVirtual.DelayedCall(ItemUsingTime, Finish);
        }
    }

    public virtual void Finish()
    {
        ItemState = ITEMSTATE.Finish;
    }

    public bool CanUsingItem()
    {
        return ItemState == ITEMSTATE.CanUse;
    }

    public bool IsUnUsedItem()
    {
        return ItemState == ITEMSTATE.UnUsed;
    }

    public bool IsUsingItem()
    {
        return ItemState == ITEMSTATE.Using;
    }

    public bool IsUsingSelectColorItem(BombBase.ENEMYCOLOR _checkColor = default)
    {
        return usingItemColor == _checkColor;
    }
}
