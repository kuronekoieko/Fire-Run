using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create StageGimmicksSO", fileName = "StageGimmicksSO")]
public class StageGimmicksSO : ScriptableObject
{
    public StageGimmick[] gimmicks;

    private static StageGimmicksSO _i;
    public static StageGimmicksSO i
    {
        get
        {
            string PATH = "ScriptableObjects/" + nameof(StageGimmicksSO);
            //初アクセス時にロードする
            if (_i == null)
            {
                _i = Resources.Load<StageGimmicksSO>(PATH);

                //ロード出来なかった場合はエラーログを表示
                if (_i == null)
                {
                    Debug.LogError(PATH + " not found");
                }
            }

            return _i;
        }
    }
}

[System.Serializable]
public struct StageGimmick
{
    public GameObject gimmickObj;
    public string key;
    public string memo;
}
