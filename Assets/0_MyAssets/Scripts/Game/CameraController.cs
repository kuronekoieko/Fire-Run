using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Unityで解像度に合わせて画面のサイズを自動調整する
/// http://www.project-unknown.jp/entry/2017/01/05/212837
/// </summary>
public class CameraController : MonoBehaviour
{
    public static CameraController i;
    Vector3 offset;
    Vector3 pos;
    Vector3 humanPos;
    public Camera mainCam { get; private set; }

    void Awake()
    {
        if (i == null) i = this;
        mainCam = GetComponent<Camera>();
    }

    void Start()
    {
        offset = transform.position - HumansManager.i.firstHumanPos;
        pos = transform.position;
        humanPos = HumansManager.i.firstHumanPos;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    /// <summary>
    /// [Unity]カメラのガタつきを修正する
    /// http://koganegames.blog.fc2.com/blog-entry-67.html
    /// </summary>
    void LateUpdate()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        if ((HumansManager.i.firstHumanPos + offset).z < pos.z)
        {
            return;
        }
        humanPos.z = HumansManager.i.firstHumanPos.z;
        pos = humanPos + offset;
        transform.position = pos;
        GameCanvasManager.i?.SetHumanCountPos(mainCam, HumansManager.i.firstHumanPos);
    }
}
