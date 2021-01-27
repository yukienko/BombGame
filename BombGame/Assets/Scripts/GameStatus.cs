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

    private GAMEPHASE _gamePhase;
    public GAMEPHASE GamePhase { 
        get { return _gamePhase; } 
        set { _gamePhase = value; Debug.Log("GamePhase:" + _gamePhase); } 
    }
}
