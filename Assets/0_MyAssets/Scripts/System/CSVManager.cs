using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVManager : MonoBehaviour
{

    void Awake()
    {
        SetStageDatas();
    }

    void SetStageDatas()
    {
        TextAsset[] stageCSVFiles = Resources.LoadAll<TextAsset>("Stages");
        Variables.stageDatas = new ObjInfo[stageCSVFiles.Length][,];
        for (int i = 0; i < stageCSVFiles.Length; i++)
        {
            Variables.stageDatas[i] = GetStageData(stageCSVFiles[i]);
        }
    }

    ObjInfo[,] GetStageData(TextAsset stageDataCSV)
    {
        var stageDataStrList = CsvToStrList(stageDataCSV);
        var stageData = new ObjInfo[stageDataStrList.Count, stageDataStrList[0].Length];
        for (int iy = stageData.GetLength(0) - 1; iy > -1; iy--)
        {
            for (int ix = 0; ix < stageData.GetLength(1); ix++)
            {
                stageData[stageData.GetLength(0) - 1 - iy, ix] = GetObjInfo(stageDataStrList[iy][ix]);
            }
        }
        return stageData;
    }

    List<string[]> CsvToStrList(TextAsset csvFile)
    {
        var strList = new List<string[]>();
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            strList.Add(line.Split(',')); // , 区切りでリストに追加
        }
        return strList;
    }

    ObjInfo GetObjInfo(string str)
    {
        //TODO:サイズを固定したい
        string[] datas = str.Split('_');

        //stringをそれぞれの型にパースする
        ObjInfo objInfo = new ObjInfo();
        objInfo.key = datas[0];
        if (objInfo.key == "")
        {
            return objInfo;
        }

        if (int.TryParse(datas[1], out int option))
        {
            objInfo.option = option;
        }

        return objInfo;
    }

}
