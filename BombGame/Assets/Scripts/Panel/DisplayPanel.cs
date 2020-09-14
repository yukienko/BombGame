using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPanel : MonoBehaviour
{
    public enum PANEL
    {
        show,
        hide,
    }

    public PANEL e_panel { get; set; } = PANEL.hide;

    [SerializeField] private Button closeButton = default;
    [SerializeField] private bool doPauseGame = false;

    private void Start()
    {
        if (closeButton) closeButton.onClick.AddListener(OnClickCloseButton);

        Init();
    }

    protected virtual void Init()
    {
        if (e_panel == PANEL.hide)
        {
            Hide();
        }
    }

    protected virtual void OnClickCloseButton()
    {
        Hide();
    }

    public void OnClickToggleButton()
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

    private void Show()
    {
        e_panel = PANEL.show;
        ShowPanel();

        if (doPauseGame) return;
        PauseController.Pause();
    }

    private void Hide()
    {
        e_panel = PANEL.hide;
        HidePanel();

        if (doPauseGame) return;
        PauseController.Resume();
    }

    protected virtual void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    protected virtual void HidePanel()
    {
        gameObject.SetActive(false);
    }
}