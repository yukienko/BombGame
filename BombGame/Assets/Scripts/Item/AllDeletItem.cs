using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDeletItem : ItemBase
{
    public override void UseItem()
    {
        base.UseItem();
        AllDelete();
    }

    private void AllDelete()
    {
        Debug.Log("デレて");
        Finish();
    }
}
