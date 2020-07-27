using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public Item itemName { get; set; }
    public enum Item
    {
        stopTime,
        barrier,
        decreaseEnemyLimit,
        allDelete
    }

    public ItemState itemState { get; set; }
    public enum ItemState
    {
        unused,
        isUse,
        finish,
    }

    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        itemState = ItemState.unused;
    }

    public virtual void UseItem()
    {
        itemState = ItemState.isUse;
    }

    public virtual void Finish()
    {
        itemState = ItemState.finish;
    }
}
