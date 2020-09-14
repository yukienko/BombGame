using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationPanel : DisplayPanel
{
    private const float AnimationTime = 0;
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
        animationPanelTransform.DOScale(1, AnimationTime);
    }

    protected override void HidePanel()
    {
        var hideAnimationTween = animationPanelTransform.DOScale(0, AnimationTime);
        hideAnimationTween.OnComplete(OnCompleteHideAnimation);
    }

    protected virtual void OnCompleteHideAnimation()
    {
        transform.SetParent(staticObjecctCanvas);
    }
}
