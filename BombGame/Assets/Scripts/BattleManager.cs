using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private void Start()
    {
        EventManager.StartListening(EventManager.Events.GameOver, GameOver);
    }

    private void GameOver()
    {
        GameStatus.Instance.GamePhase = GameStatus.GAMEPHASE.GameOver;
    }
}
