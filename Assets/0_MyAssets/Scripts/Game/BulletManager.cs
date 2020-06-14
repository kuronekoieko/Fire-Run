using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BulletManager : MonoBehaviour
{
    [SerializeField] BulletController bulletPrefab;
    List<BulletController> bulletControllers;
    float timer;
    float shootIntervalSec = 0.1f;

    void Awake()
    {
        bulletControllers = new List<BulletController>();
        for (int i = 0; i < 10; i++)
        {
            bulletControllers.Add(Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity));
            bulletControllers[i].OnInstantiate(transform);
        }
    }
    void Start()
    {
        timer = shootIntervalSec;
    }


    void Update()
    {
        ShootTimer();
    }

    void ShootTimer()
    {
        timer += Time.deltaTime;
        if (timer < shootIntervalSec) { return; }
        timer = 0;
        var bullet = bulletControllers.Where(b => !b.gameObject.activeSelf).FirstOrDefault();
        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.OnInstantiate(transform);
            bulletControllers.Add(bullet);
        }
        bullet.Shoot();
    }


}
