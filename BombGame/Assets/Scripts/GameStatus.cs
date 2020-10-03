using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : Singleton<GameStatus>
{
    public enum GAMEPHASE
    {
        Wait,
        Start,
        BombExploded, //ボム爆発
        GameOver,
        Result,
    }

    public GAMEPHASE GamePhase { get; set; } = default;
}
