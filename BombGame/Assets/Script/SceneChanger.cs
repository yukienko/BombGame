using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger
{
    private static readonly string TitleSceneName = "TitleScene";
    private static readonly string GameSceneName = "GameScene";

    public static void Title()
    {
        SceneManager.LoadScene(TitleSceneName);
    }

    public static void Game()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}
