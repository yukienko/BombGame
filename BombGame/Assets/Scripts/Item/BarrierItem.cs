using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierItem : ItemBase
{
    public override void UseItem()
    {
        base.UseItem();
        Barrier();
    }

    private void Barrier()
    {
        Debug.Log("バーリア！");
        Finish();
    }
}
