using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimeItem : ItemBase
{
    private const float StopTimeCount = 5.0f;

    protected override ItemState.ITEMSTATE itemState
    {
        get => ItemState.StopTimeItemState;
        set
        {
            ItemState.StopTimeItemState = value;
            EventManager.TriggerEvent(EventManager.Events.StopTimeItem);
        }
    }

    private void Awake()
    {
        base.itemUsingTime = StopTimeCount;
    }
}
