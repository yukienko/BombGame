using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectColorItemController : MonoBehaviour
{
    [SerializeField] Button BlueMoveItemButton = default;
    [SerializeField] Button GreenMoveItemButton = default;
    [SerializeField] Button RedMoveItemButton = default;
    [SerializeField] Button YellowMoveItemButton = default;

    public event Action<BombBase.ENEMYCOLOR> UseItemAction = default;

    void Start()
    {
        if (BlueMoveItemButton) BlueMoveItemButton.onClick.AddListener(() => UseItemAction(BombBase.ENEMYCOLOR.blue));
        if (GreenMoveItemButton) GreenMoveItemButton.onClick.AddListener(() => UseItemAction(BombBase.ENEMYCOLOR.green));
        if (RedMoveItemButton) RedMoveItemButton.onClick.AddListener(() => UseItemAction(BombBase.ENEMYCOLOR.red));
        if (YellowMoveItemButton) YellowMoveItemButton.onClick.AddListener(() => UseItemAction(BombBase.ENEMYCOLOR.yellow));
    }
}
