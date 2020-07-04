using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rb;
    public float Speed { get; set; }
    Vector3 offset = new Vector3(0, 1, 0);
    Transform humanTfm;
    float timer;
    float shootIntervalSec = 2;
    public void OnInstantiate(Transform humanTfm, float speed)
    {
        Speed = speed;
        this.humanTfm = humanTfm;
        transform.position = humanTfm.position + offset;
        gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * Speed;
        ShootTimer();
    }


    public void Shoot(float angle)
    {
        timer = 0;
        gameObject.SetActive(true);
        transform.position = humanTfm.position + offset;
        var eulerAngles = transform.eulerAngles;
        eulerAngles.y = angle;
        var direction = Quaternion.Euler(eulerAngles) * Vector3.forward;
        transform.forward = direction;
    }

    void ShootTimer()
    {
        timer += Time.deltaTime;
        if (timer < shootIntervalSec) { return; }
        timer = 0;

        gameObject.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        var block = other.gameObject.GetComponent<BlockController>();
        if (block == null) { return; }
        gameObject.SetActive(false);

    }
}
