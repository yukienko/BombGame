using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState
{
    public enum ITEMSTATE
    {
        canUse,
        unUsed,
        isUse,
        finish,
    }

    public static ITEMSTATE StopTimeItemState;
    public static ITEMSTATE AllDeleteItemState;
    public static ITEMSTATE BarrierItemState;
    public static ITEMSTATE DecreaseEnemyLimitItemState;
}
