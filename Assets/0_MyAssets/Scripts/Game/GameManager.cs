using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 3D空間の処理の管理
/// </summary>
public class GameManager : MonoBehaviour
{
    GameObject[] stages;
    [SerializeField] HumansManager _humansManager;
    public HumansManager humansManager => _humansManager;
    public static GameManager i;

    void Start()
    {
        i = i ?? this;
        //stages = Resources.LoadAll("Stages", typeof(GameObject)).Cast<GameObject>().ToArray();
        //Variables.lastStageIndex = stages.Length - 1;
        //Instantiate(stages[Variables.currentStageIndex], Vector3.zero, Quaternion.identity);

    }

    [ContextMenu("ChangeBlocksColor")]
    void ChangeBlocksColor()
    {
        var blocks = FindObjectsOfType<BlockController>();
        Array.ForEach(blocks, b =>
        {
            b.SetView(b.getHp);
        });
    }
}
