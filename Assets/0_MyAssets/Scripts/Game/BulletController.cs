using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rb;
    float speed = 30f;
    Vector3 offset = new Vector3(0, 1, 0);
    Transform humanTfm;
    float timer;
    float shootIntervalSec = 1;
    public void OnStart(Transform humanTfm)
    {
        this.humanTfm = humanTfm;
        transform.position = humanTfm.position + offset;
        gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;
        //ShootTimer();
    }


    public void Shoot()
    {
        gameObject.SetActive(true);
        transform.position = humanTfm.position + offset;
    }

    void ShootTimer()
    {
        timer += Time.deltaTime;
        if (timer < shootIntervalSec) { return; }
        timer = 0;

        gameObject.SetActive(true);
        transform.position = humanTfm.position + offset;
    }


    void OnTriggerEnter(Collider other)
    {
        var block = other.gameObject.GetComponent<BlockController>();
        if (block == null) { return; }
        gameObject.SetActive(false);

    }
}
