using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDeletItem : ItemBase
{
    private const float AllDeleteTime = 1.0f;

    protected override ItemState.ITEMSTATE itemState
    {
        get => ItemState.AllDeleteItemState;
        set
        {
            ItemState.AllDeleteItemState = value;
            EventManager.TriggerEvent(EventManager.Events.AllDeleteItem);
        }
    }

    private void Awake()
    {
        base.itemUsingTime = AllDeleteTime;
    }
}
