using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonController : MonoBehaviour
{
    // 時間停止
    [SerializeField] Button StopItemButton = default;
    // 選択した色のボムを除去
    [SerializeField] Button MoveItemButton = default;
    // 選択した色のボムを生成
    [SerializeField] Button GenerateItemButton = default;

    [SerializeField] SelectColorItemController SelectColorItemControllerPanel = default;

    private void Start()
    {
        if (StopItemButton) StopItemButton.onClick.AddListener(UseStopItem);
        if (MoveItemButton) MoveItemButton.onClick.AddListener(SetUpSelectColorMoveItem);
        if (GenerateItemButton) GenerateItemButton.onClick.AddListener(SetUpSelectColorGenerateItem);

        InitItems();
    }

    private void InitItems()
    {
        StopTimeItem.Instance.Init();
        SelectColorMoveItem.Instance.Init();
        SelectColorGenerateItem.Instance.Init();
        BarrierItem.Instance.Init();
    }

    private void UseStopItem()
    {
        StopTimeItem.Instance.UseItem();
    }

    private void SetUpSelectColorMoveItem()
    {
        if (!SelectColorMoveItem.Instance.CanUsingItem())
        {
            return;
        }
        SetItemPanel(true, UseSelectColorMoveItem);
    }

    private void UseSelectColorMoveItem(BombBase.ENEMYCOLOR _selectColor)
    {
        SelectColorMoveItem.Instance.SetSelectColorNumber(_selectColor);
        SelectColorMoveItem.Instance.UseItem();
        Debug.Log("UseMoveItem : "+_selectColor);
        SetItemPanel(false, UseSelectColorMoveItem);
    }

    private void SetUpSelectColorGenerateItem()
    {
        if (!SelectColorGenerateItem.Instance.CanUsingItem())
        {
            return;
        }
        SetItemPanel(true, UseSelectColorGenerateItem);
    }

    private void UseSelectColorGenerateItem(BombBase.ENEMYCOLOR _selectColor)
    {
        SelectColorGenerateItem.Instance.SetSelectColorNumber(_selectColor);
        SelectColorGenerateItem.Instance.UseItem();
        Debug.Log("UseGenerateItem : " + _selectColor);
        SetItemPanel(false, UseSelectColorGenerateItem);
    }

    private void SetItemPanel(bool active, System.Action<BombBase.ENEMYCOLOR> itemAction)
    {
        SelectColorItemControllerPanel.gameObject.SetActive(active);
        if (active)
        {
            SelectColorItemControllerPanel.UseItemAction += itemAction;
        }
        else
        {
            SelectColorItemControllerPanel.UseItemAction -= itemAction;
        }
    }
}
