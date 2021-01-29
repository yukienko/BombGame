using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColorMoveItem : ActiveItemBase<SelectColorMoveItem>
{
    private const float SelectColorMoveTime = 1.0f;
    protected override float ItemUsingTime
    {
        get => SelectColorMoveTime;
        set => ItemUsingTime = value;
    }

    protected override ITEMSTATE ItemState
    {
        get => base.ItemState;
        set
        {
            base.ItemState = value;
            EventManager.TriggerEvent(EventManager.Events.SelectColorMoveItem);
        }
    }

    public void SetSelectColorNumber(BombBase.ENEMYCOLOR _selectColor)
    {
        usingItemColor = _selectColor;
    }
}
