using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace conv
{
    public class ConvenientAssets
    {
        /// <summary>
        /// bool型の乱数を取得する
        /// </summary>
        /// <returns>bool型の乱数</returns>
        public static bool RandomBool()
        {
            return Random.Range(0, 2) == 0;
        }

        /// <summary>
        /// float型の乱数を取得する
        /// </summary>
        /// <returns>float型の乱数</returns>
        public static float RandomFloat(float min, float max)
        {
            //!min以上max以下
            return Random.Range(min, max);
        }

        /// <summary>
        /// int型の乱数を取得する
        /// </summary>
        /// <returns>int型の乱数</returns>
        public static int RandomInt(int min, int max)
        {
            //!min以上max未満
            return Random.Range(min, max);
        }
    }
}