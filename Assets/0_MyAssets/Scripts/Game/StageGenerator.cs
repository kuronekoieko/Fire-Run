using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{

    public void Generate()
    {
        int stageIndex = 0;
        ObjInfo[,] stageDatas = Variables.stageDatas[stageIndex];
        for (int iz = 0; iz < stageDatas.GetLength(0); iz++)
        {
            Debug.Log(iz + " ====================");
            for (int ix = 0; ix < stageDatas.GetLength(1); ix++)
            {
                Debug.Log(stageDatas[iz, ix].key);
            }
        }
    }
}
