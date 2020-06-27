using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageGenerator : MonoBehaviour
{

    public void Generate()
    {
        ObjInfo[,] stageData = Variables.stageDatas[Variables.currentStageIndex];
        Vector3 pos = Vector3.zero;
        for (int iz = 0; iz < stageData.GetLength(0); iz++)
        {
            // Debug.Log(iz + " ====================");
            float offset = 2.1f;
            for (int ix = 0; ix < stageData.GetLength(1); ix++)
            {
                //Debug.Log(stageDatas[iz, ix].key);
                pos.x = -offset * 2 + (float)ix * offset;
                pos.y = offset / 2f;
                pos.z = iz * offset;
                GenerateObj(stageData[iz, ix], pos);
            }
        }
    }

    void GenerateObj(ObjInfo objInfo, Vector3 pos)
    {
        StageGimmick gimmick = StageGimmicksSO.i.gimmicks.Where(g => g.key == objInfo.key).FirstOrDefault();
        if (gimmick.gimmickObj == null) { return; }
        GameObject obj = Instantiate(gimmick.gimmickObj, pos, Quaternion.identity);
        //TODO:いつか直す
        switch (gimmick.key)
        {
            case "b":
                obj.GetComponent<BlockController>().OnInstantitate(objInfo.option);
                break;
            case "i":
                obj.GetComponent<ItemController>().OnInstantitate(objInfo.option);
                break;
            default:
                break;
        }
    }
}
