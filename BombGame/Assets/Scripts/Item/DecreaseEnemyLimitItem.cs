using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseEnemyLimitItem : ItemBase
{
    protected override ItemState.ITEMSTATE itemState
    {
        get => ItemState.DecreaseEnemyLimitItemState;
        set => ItemState.DecreaseEnemyLimitItemState = value;
    }
}
