using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseEnemyLimitItem : ItemBase
{
    public override void UseItem()
    {
        base.UseItem();
        DecreaseEnemyLimit();
    }

    private void DecreaseEnemyLimit()
    {
        Debug.Log("これだけパッシブなんでややこしいよ");
        Finish();
    }
}
