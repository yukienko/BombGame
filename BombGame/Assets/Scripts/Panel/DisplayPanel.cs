﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static hoge;

public class DisplayPanel : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (e_panel == PANEL.hide)
        {
            Hide();
        }
    }

    public void Toggle()
    {
        if (e_panel == PANEL.hide)
        {
            Show();
        }
        else if (e_panel == PANEL.show)
        {
            Hide();
        }
    }

    protected virtual void Show()
    {
        gameObject.SetActive(true);
        e_panel = PANEL.show;
    }

    protected virtual void Hide()
    {
        gameObject.SetActive(false);
        e_panel = PANEL.hide;
    }
}

public class hoge
{
    public enum PANEL
    {
        show,
        hide,
    }

    public static PANEL e_panel { get; set; } = PANEL.hide;
};