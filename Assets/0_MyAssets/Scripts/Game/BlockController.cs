using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using System.Linq;
public class BlockController : MonoBehaviour
{

    [SerializeField] int hp;
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] TextMeshPro textMeshPro1;
    [SerializeField] ParticleSystem ps;
    [SerializeField] ColorProperty[] colorProperty;
    [SerializeField] MeshRenderer meshRenderer;
    public int getHp => hp;
    void Start()
    {
        this.ObserveEveryValueChanged(hp => this.hp)
            .Subscribe(hp => SetView(hp))
            .AddTo(this.gameObject);
        ps.GetComponent<Renderer>().material.color = colorProperty[0].color;
    }

    public void OnInstantitate(int hp)
    {
        this.hp = hp;
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
        meshRenderer.material.color = colorProperty.Where(c => c.minHp <= hp).LastOrDefault().color;
    }
}

[System.Serializable]
public struct ColorProperty
{
    public Color color;
    public int minHp;
}