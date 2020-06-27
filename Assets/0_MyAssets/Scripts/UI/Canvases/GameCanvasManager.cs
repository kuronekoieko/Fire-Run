using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using DG.Tweening;

/// <summary>
/// ゲーム画面
/// ゲーム中に表示するUIです
/// あくまで例として実装してあります
/// ボタンなどは適宜編集してください
/// </summary>
public class GameCanvasManager : BaseCanvasManager
{
    [SerializeField] TextMeshProUGUI stageNumText;
    [SerializeField] TextMeshProUGUI humanCountText;
    [SerializeField] TextMeshProUGUI addCountText;
    public static GameCanvasManager i;

    public override void OnStart()
    {
        i = i ?? this;
        base.SetScreenAction(thisScreen: ScreenState.Game);

        this.ObserveEveryValueChanged(currentStageIndex => Variables.currentStageIndex)
            .Subscribe(currentStageIndex => { ShowStageNumText(); })
            .AddTo(this.gameObject);

        this.ObserveEveryValueChanged(humanCount => Variables.humanCount)
            .Subscribe(humanCount => humanCountText.text = humanCount.ToString())
            .AddTo(this.gameObject);

        gameObject.SetActive(true);

    }

    public override void OnUpdate()
    {
        if (!base.IsThisScreen()) { return; }
    }

    void LateUpdate()
    {

    }

    public void SetHumanCountPos(Camera mainCam, Vector3 targetPos)
    {
        var pos = RectTransformUtility.WorldToScreenPoint(mainCam, targetPos);
        pos.y += 150;
        humanCountText.rectTransform.position = pos;
    }

    public void ShowAddText(Camera mainCam, Vector3 targetPos)
    {
        var tmpUGUI = Instantiate(addCountText, Vector3.zero, Quaternion.identity, transform);
        var pos = RectTransformUtility.WorldToScreenPoint(mainCam, targetPos);
        pos.x += 100;
        tmpUGUI.rectTransform.position = pos;

        Sequence sequence = DOTween.Sequence()
            .Append(tmpUGUI.rectTransform.DOLocalMoveY(100, 0.5f).SetRelative())
            .Append(DOTween.ToAlpha(() => tmpUGUI.color, color => tmpUGUI.color = color, 0, 0.5f));
    }


    public override void OnInitialize()
    {

    }

    protected override void OnOpen()
    {
        gameObject.SetActive(true);
    }

    protected override void OnClose()
    {
        // gameObject.SetActive(false);
    }

    void ShowStageNumText()
    {
        stageNumText.text = "LEVEL " + (Variables.currentStageIndex + 1).ToString("000");
    }
}
