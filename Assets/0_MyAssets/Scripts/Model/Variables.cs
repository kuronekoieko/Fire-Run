using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム内で使う変数
/// UIに表示するときはUniRxで値を監視するのがおすすめ
/// ・Unityで学ぶMVPパターン ~ UniRxを使って体力Barを作成する ~
/// https://qiita.com/Nakashima_Hibari/items/5e0c36c3b0df78110d32
/// </summary>
public class Variables : MonoBehaviour
{
    public static ScreenState screenState = ScreenState.Game;
    public static int currentStageIndex
    {
        set { _currentstageIndex = Mathf.Clamp(value, 0, stageDatas.Length - 1); }
        get { return _currentstageIndex; }
    }
    private static int _currentstageIndex;
    public static int humanCount;
    public static ObjInfo[][,] stageDatas;
}


public struct ObjInfo
{
    public string key;
    public int option;
}
