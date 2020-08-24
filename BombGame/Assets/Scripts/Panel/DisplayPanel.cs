using System.Collections;
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
        Hide();
    }

    public void Toggle()
    {
        if (e_panel == PANEL.hide)
        {
            Show();
            Debug.Log("見える処理" + e_panel);
        }
        else if (e_panel == PANEL.show)
        {
            Hide();
            Debug.Log("消える処理" + e_panel);
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

    public static PANEL e_panel { get; set; }
};