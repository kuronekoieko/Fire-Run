﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum ItemType
{
    TimeInterval,
    AddSimultaneousCount,
}
public class ItemController : MonoBehaviour
{
    [SerializeField] ItemType itemType;
    public ItemType ItemType => itemType;
    Tween rotateTween;
    void Start()
    {
        rotateTween = transform.DORotate(new Vector3(0, 360, 0), 2).SetEase(Ease.Linear).SetLoops(-1).SetRelative();
    }
    void OnTriggerEnter(Collider other)
    {
        GetItem(other);
    }

    void GetItem(Collider other)
    {
        var bulletManager = other.GetComponent<BulletManager>();
        if (bulletManager == null) { return; }
        switch (ItemType)
        {
            case ItemType.TimeInterval:
                bulletManager.ShortenTimeInterval();
                break;
            case ItemType.AddSimultaneousCount:
                bulletManager.AddSimultaneousCount();
                break;
            default:
                break;
        }
        gameObject.SetActive(false);
        rotateTween.Kill();
    }
}
