using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] Button StopItemButton = default;
    [SerializeField] Button DeleteItemButton = default;

    private void Start()
    {
        if (StopItemButton) StopItemButton.onClick.AddListener(UseStopItem);
        if (DeleteItemButton) DeleteItemButton.onClick.AddListener(UseDeleteItem);

        InitItems();
    }

    private void InitItems()
    {
        StopTimeItem.Instance.Init();
        AllDeletItem.Instance.Init();
        BarrierItem.Instance.Init();
        DecreaseEnemyLimitItem.Instance.Init();
    }

    private void UseStopItem()
    {
        StopTimeItem.Instance.UseItem();
    }

    private void UseDeleteItem()
    {
        AllDeletItem.Instance.UseItem();
    }
}
