using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDeletItem : ItemBase<AllDeletItem>
{
    private const float AllDeleteTime = 1.0f;
    protected override float ItemUsingTime
    {
        get => AllDeleteTime;
        set => ItemUsingTime = value;
    }

    protected override ITEMSTATE ItemState
    {
        get => base.ItemState;
        set
        {
            base.ItemState = value;
            EventManager.TriggerEvent(EventManager.Events.AllDeleteItem);
        }
    }
}
