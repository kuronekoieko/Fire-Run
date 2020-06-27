using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageGenerator : MonoBehaviour
{

    public void Generate()
    {
        int stageIndex = 0;
        ObjInfo[,] stageData = Variables.stageDatas[stageIndex];
        for (int iz = 0; iz < stageData.GetLength(0); iz++)
        {
            // Debug.Log(iz + " ====================");
            float offset = 2.1f;
            for (int ix = 0; ix < stageData.GetLength(1); ix++)
            {
                //Debug.Log(stageDatas[iz, ix].key);
                Vector3 pos = new Vector3(-offset * 2 + (float)ix * offset, offset / 2f, iz * offset);
                StageGimmick gimmick = StageGimmicksSO.i.gimmicks.Where(g => g.key == stageData[iz, ix].key).FirstOrDefault();
                if (gimmick.gimmickObj == null) { continue; }
                Instantiate(gimmick.gimmickObj, pos, Quaternion.identity);

            }
        }
    }
}
