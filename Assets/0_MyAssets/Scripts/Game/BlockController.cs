using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
public class BlockController : MonoBehaviour
{

    [SerializeField] int hp;
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] TextMeshPro textMeshPro1;
    [SerializeField] ParticleSystem ps;
    [SerializeField] Color[] colors;
    [SerializeField] MeshRenderer meshRenderer;
    public int getHp => hp;
    void Start()
    {
        this.ObserveEveryValueChanged(hp => this.hp)
            .Subscribe(hp => SetView(hp))
            .AddTo(this.gameObject);
        ps.GetComponent<Renderer>().material.color = colors[0];
    }


    void Update()
    {

    }

    void OnCollisionEnter(Collision collisionInfo)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        var bullet = other.gameObject.GetComponent<BulletController>();
        if (bullet == null) { return; }
        hp--;
        if (hp == 0)
        {
            var ps = Instantiate(this.ps, transform.position, Quaternion.identity);
            ps.Play();
            gameObject.SetActive(false);
        }
    }

    public void SetView(int hp)
    {
        textMeshPro1.text = hp.ToString();
        textMeshPro.text = hp.ToString();
        int index = Mathf.FloorToInt(hp / 5);
        if (colors.Length - 1 < index) { index = colors.Length - 1; }
        meshRenderer.material.color = colors[index];
        ps.GetComponent<Renderer>().material.color = colors[index];
    }
}
