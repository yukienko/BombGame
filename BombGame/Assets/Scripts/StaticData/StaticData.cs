using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StaticManager
{
    public class ResolutionData
    {
        //全体の解像度比率
        public static readonly Vector2 Resolution = new Vector2(1920, 1080);
        //UI部分の解像度比率
        public static readonly Vector2 UIResolutionRect = new Vector2(20, 15);
        //解像度比率
        public static readonly Vector2 ResolutionRect = new Vector2(16, 9);
        //ゲームシーンの解像度
        public static readonly Vector2 GameSceneResolution = new Vector2(Screen.width, Screen.height);
        //画面解像度　↓を有効化したらプロジェクトぶっ壊れます
        //public static readonly Vector2 OverallResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        //ボタン部分のＵＩの比率(100分率)
        public static readonly float SideUIRect = 0.05f;
        //スコア部分のUIの比率
        public static readonly float UpperUIRect = 0.05f;
    }

    public class GameRect
    {

    }
}