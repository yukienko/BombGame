using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierItem : ItemBase
{
    protected override ItemState.ITEMSTATE itemState
    {
        get => ItemState.BarrierItemState;
        set => ItemState.BarrierItemState = value;
    }
}
