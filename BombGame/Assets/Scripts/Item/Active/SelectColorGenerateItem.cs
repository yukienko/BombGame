using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColorGenerateItem : ActiveItemBase<SelectColorGenerateItem>
{
    private const float SelectColorGenerateTime = 1.0f;
    protected override float ItemUsingTime
    {
        get => SelectColorGenerateTime;
        set => ItemUsingTime = value;
    }

    protected override ITEMSTATE ItemState
    {
        get => base.ItemState;
        set
        {
            base.ItemState = value;
            EventManager.TriggerEvent(EventManager.Events.SelectColorGenerateItem);
        }
    }

    public void SetSelectColorNumber(BombBase.ENEMYCOLOR _selectColor)
    {
        usingItemColor = _selectColor;
    }
}
