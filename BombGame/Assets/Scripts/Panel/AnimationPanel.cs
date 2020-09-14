using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPanel : DisplayPanel
{
    float animationTime = 0;
    protected RectTransform animationPanelTransform = default;

    [SerializeField] private Transform canvas = default;
    [SerializeField] private Transform staticObjecctCanvas = default;

    protected override void Init()
    {
        SetAnimationTransform();
        base.Init();
    }

    protected virtual void SetAnimationTransform()
    {
        animationPanelTransform = gameObject.GetComponent<RectTransform>();
    }

    protected override void ShowPanel()
    {
        transform.SetParent(canvas);
        animationPanelTransform.
    }

    protected override void HidePanel()
    {
        base.HidePanel();
    }

    protected virtual void OnCompleteHideAnimation()
    {
        transform.SetParent(staticObjecctCanvas);
    }
}
