using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimeItem : ItemBase
{
    private const float StopTimeCount = 5.0f;
    public override void UseItem()
    {
        base.UseItem();
        StopTime();
    }

    private void StopTime()
    {
        Invoke("Finish", StopTimeCount);
    }
}
