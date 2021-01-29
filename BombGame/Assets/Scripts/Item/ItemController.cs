using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    // 時間停止
    [SerializeField] Button StopItemButton = default;
    // 選択した色を除去
    [SerializeField] Button BlueMoveItemButton = default;
    [SerializeField] Button GreenMoveItemButton = default;
    [SerializeField] Button RedMoveItemButton = default;
    [SerializeField] Button YellowMoveItemButton = default;

    private void Start()
    {
        if (StopItemButton) StopItemButton.onClick.AddListener(UseStopItem);
        if (BlueMoveItemButton) BlueMoveItemButton.onClick.AddListener(() => UseSelectColorMoveItem(BombBase.ENEMYCOLOR.blue));
        if (GreenMoveItemButton) GreenMoveItemButton.onClick.AddListener(() => UseSelectColorMoveItem(BombBase.ENEMYCOLOR.green));
        if (RedMoveItemButton) RedMoveItemButton.onClick.AddListener(() => UseSelectColorMoveItem(BombBase.ENEMYCOLOR.red));
        if (YellowMoveItemButton) YellowMoveItemButton.onClick.AddListener(() => UseSelectColorMoveItem(BombBase.ENEMYCOLOR.yellow));

        InitItems();
    }

    private void InitItems()
    {
        StopTimeItem.Instance.Init();
        SelectColorMoveItem.Instance.Init();
        BarrierItem.Instance.Init();
        SelectColorGenerateItem.Instance.Init();
    }

    private void UseStopItem()
    {
        StopTimeItem.Instance.UseItem();
    }

    private void UseSelectColorMoveItem(BombBase.ENEMYCOLOR _selectColor)
    {
        if(!SelectColorMoveItem.Instance.CanUsingItem())
        {
            return;
        }
        SelectColorMoveItem.Instance.SetSelectColorNumber(_selectColor);
        SelectColorMoveItem.Instance.UseItem();
        Debug.Log("UseMoveItem : "+_selectColor);
    }
}
