using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    TimeInterval,
    AddSimultaneousCount,
}
public class ItemController : MonoBehaviour
{
    [SerializeField] ItemType itemType;
    public ItemType ItemType => itemType;
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
    }
}
