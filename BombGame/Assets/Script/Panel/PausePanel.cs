using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : DisplayPanel
{
    protected override void Init()
    {
        base.Init();
        Time.timeScale = 1f;
    }

    protected override void Hide()
    {
        base.Hide();
        Time.timeScale = 1f;
    }

    protected override void Show()
    {
        base.Show();
        Time.timeScale = 0f;
    }
}
