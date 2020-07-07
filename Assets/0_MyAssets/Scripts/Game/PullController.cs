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
        //PutOn();
        //Pull();
        // PutSide();
    }
    void LateUpdate()
    {
        // PutSide();
    }

    void PutSide()
    {
        if (follower == null) { return; }
        follower.transform.position = transform.position + Vector3.right * HumansManager.i.distance;
        follower.GetRigidbody.isKinematic = true;
    }

    void PutOn()
    {
        if (follower == null) { return; }
        follower.transform.position = transform.position + Vector3.up * 2;
        follower.GetRigidbody.isKinematic = true;
    }
}
