using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimeItem : ItemBase<StopTimeItem>
{
    private const float StopTimeCount = 5.0f;
    protected override float ItemUsingTime 
    { 
        get => StopTimeCount;
        set => ItemUsingTime = value;
    }

    protected override ITEMSTATE ItemState
    {
        get => base.ItemState;
        set
        {
            base.ItemState = value;
            EventManager.TriggerEvent(EventManager.Events.StopTimeItem);
        }
    }
}
