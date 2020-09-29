using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : Singleton<GameStatus>
{
    public enum GANEPHASE
    {
        Wait,
        Start,
        BombExploded, //ボム爆発
        GameOver,
        Result,
    }
}
