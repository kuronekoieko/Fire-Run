using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public struct BulletProperty
{
    public int simultaneousCount;
    public float shootIntervalSec;
}

public class BulletManager : MonoBehaviour
{
    [SerializeField] BulletController bulletPrefab;
    List<BulletController> bulletControllers;
    float timer;
    float speed = 30;
    float dAngle = 2;
    HumanController humanController;
    private BulletProperty bulletProperty;
    public BulletProperty BulletProperty
    {
        set { bulletProperty = value; }
        get { return bulletProperty; }
    }
    void Awake()
    {
        bulletProperty = new BulletProperty
        {
            shootIntervalSec = 0.5f,
            simultaneousCount = 1,
        };
        humanController = GetComponent<HumanController>();
        bulletControllers = new List<BulletController>();
        for (int i = 0; i < 10; i++)
        {
            bulletControllers.Add(Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity));
            bulletControllers[i].OnInstantiate(transform, speed);
        }
    }

    void Start()
    {
        timer = bulletProperty.shootIntervalSec;
    }

    void Update()
    {
        if (humanController.state == HumanState.Run)
        {
            ShootTimer();
        }

    }

    void ShootTimer()
    {
        timer += Time.deltaTime;
        if (timer < bulletProperty.shootIntervalSec) { return; }
        timer = 0;

        int n = bulletProperty.simultaneousCount;
        for (int i = 0; i < n; i++)
        {
            float angle = dAngle * (-Mathf.Floor(n / 2) + i + ((n + 1) % 2) / 2f);
            var bullet = bulletControllers.Where(b => !b.gameObject.activeSelf).FirstOrDefault();
            if (bullet == null)
            {
                bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
                bullet.OnInstantiate(transform, speed);
                bulletControllers.Add(bullet);
            }
            bullet.Shoot(angle);
        }
    }

    public void ShortenTimeInterval()
    {
        bulletProperty.shootIntervalSec = 0.2f;
    }

    public void AddSimultaneousCount()
    {
        bulletProperty.simultaneousCount += 1;
    }

    public void SetProperty()
    {

    }

}