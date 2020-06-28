using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class StartCanvasManager : BaseCanvasManager
{
    [SerializeField] RectTransform fingerPointRTf;
    Sequence sequence;
    Vector3 startPos;
    public override void OnStart()
    {
        base.SetScreenAction(thisScreen: ScreenState.Start);
        startPos = fingerPointRTf.anchoredPosition;
    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen()) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            Variables.screenState = ScreenState.Game;
        }
    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
        fingerPointRTf.anchoredPosition = startPos;
        sequence = DOTween.Sequence()
        .Append(fingerPointRTf.DOLocalMoveX(-startPos.x, 1).SetEase(Ease.InOutSine))
        .Append(fingerPointRTf.DOLocalMoveX(startPos.x, 1).SetEase(Ease.InOutSine));
        sequence.SetLoops(-1);
    }

    protected override void OnClose()
    {
        gameObject.SetActive(false);
        sequence.Kill();
    }
}
