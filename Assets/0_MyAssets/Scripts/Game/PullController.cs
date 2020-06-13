using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PullController : MonoBehaviour
{
    [NonSerialized] public HumanController follower;
    List<Vector3> positions;
    HumanController humanController;

    void Awake()
    {
        positions = new List<Vector3>();
        humanController = GetComponent<HumanController>();
    }
    void Start()
    {

    }


    void Update()
    {

    }


    void FixedUpdate()
    {
        //ここでnullチェックしないと人が左右にずれる
        if (follower == null) { return; }
        //if (positions[positions.Count - 1] == transform.position) { return; }
        positions.Add(humanController.rb.velocity);
        float dDistance = Time.deltaTime * humanController.speed;
        if (dDistance * positions.Count < HumansManager.i.distance) { return; }
        if (follower) follower.rb.velocity = positions[0];
        positions.RemoveAt(0);
    }
}
