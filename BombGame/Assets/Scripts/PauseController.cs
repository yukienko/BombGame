using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController
{
    public enum IsPlay
    {
        pause,
        resume,
    }

    private static IsPlay e_isPlay = IsPlay.pause;

    public static void Pause()
    {
        e_isPlay = IsPlay.pause;
    }

    public static void Resume()
    {
        e_isPlay = IsPlay.resume;
    }

    public static bool IsPause()
    {
        return e_isPlay == IsPlay.pause;
    }
}
