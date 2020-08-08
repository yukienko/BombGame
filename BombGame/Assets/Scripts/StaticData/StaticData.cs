using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StaticManager
{
    public class ResolutionData
    {
        //全体の解像度
        public static readonly Vector2 Resolution = new Vector2(1920, 1080);
        //UI部分の解像度比率
        public static readonly Vector2 UIResolutionRect = new Vector2(20, 15);
    }

    public class GameRect
    {
        public static readonly float gameRight = ResolutionData.Resolution.x - ResolutionData.Resolution.x / ResolutionData.UIResolutionRect.x;
        public static readonly float gameLeft = ResolutionData.Resolution.x / ResolutionData.UIResolutionRect.x;
        public static readonly float gameTop = ResolutionData.Resolution.y - ResolutionData.Resolution.y / ResolutionData.UIResolutionRect.y;
        public static readonly float gameButtom = 0;
    }
}