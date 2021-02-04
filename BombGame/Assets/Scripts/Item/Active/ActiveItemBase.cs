using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActiveItemBase<t> : Singleton<t> where t : class, new()
{
    public enum ITEMSTATE
    {
        CanUse,
        Using,
        Charging,
    }
    protected virtual ITEMSTATE ItemState { get; set; }
    protected virtual float ItemUsingTime { get; set; } = 0;

    protected BombBase.ENEMYCOLOR usingItemColor = default;

    private float _chargingTime = 5f;

    public void Init()
    {
        Reset();
    }

    private void Reset()
    {
        ItemState = ITEMSTATE.CanUse;
    }

    public virtual void UseItem()
    {
        ItemState = ITEMSTATE.Using;
        DOVirtual.DelayedCall(ItemUsingTime, Charging);
    }

    public virtual void Charging()
    {
        ItemState = ITEMSTATE.Charging;
        DOVirtual.DelayedCall(_chargingTime, Reset);
    }

    public bool CanUsingItem()
    {
        return ItemState == ITEMSTATE.CanUse;
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
