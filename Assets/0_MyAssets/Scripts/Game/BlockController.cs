using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using System.Linq;
using DG.Tweening;
public enum BlockType
{
    Normal,
    Needle,
    Bomb,
}
public class BlockController : MonoBehaviour
{
    [SerializeField] BlockType blockType;
    [SerializeField] int hp;
    [SerializeField] TextMeshPro textMeshPro;
    [SerializeField] TextMeshPro textMeshPro1;
    [SerializeField] ParticleSystem ps;
    [SerializeField] ColorProperty[] colorProperty;
    [SerializeField] MeshRenderer meshRenderer;
    public int getHp => hp;
    float moveDistance;
    void Start()
    {
        this.ObserveEveryValueChanged(hp => this.hp)
            .Subscribe(hp => SetView(hp))
            .AddTo(this.gameObject);
        ps.GetComponent<Renderer>().material.color = colorProperty[0].color;
        if (blockType == BlockType.Needle)
        {
            Sequence sequence = DOTween.Sequence()
            .Append(transform.DOLocalMoveZ(moveDistance, 1).SetRelative().SetEase(Ease.Linear))
            .Append(transform.DOLocalMoveZ(-moveDistance, 1).SetRelative().SetEase(Ease.Linear));
        }
    }

    public void OnInstantitate(int option, float offset)
    {
        this.hp = option;
        moveDistance = offset;
    }


    void Update()
    {
    }

    void OnCollisionEnter(Collision collisionInfo)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (blockType == BlockType.Needle) { return; }
        var bullet = other.gameObject.GetComponent<BulletController>();
        if (bullet == null) { return; }
        hp--;
        if (hp == 0)
        {
            Broken();
        }
    }

    public void Broken()
    {
        var ps = Instantiate(this.ps, transform.position, Quaternion.identity);
        ps.Play();
        gameObject.SetActive(false);

        if (blockType == BlockType.Bomb)
        {
            Explosion();
        }
    }

    void Explosion()
    {

        var blocks = GetBlocks(-Vector3.right * 2.1f * 4);
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].Broken();
        }
        blocks = GetBlocks(Vector3.right * 2.1f * 4);
        for (int i = 0; i < blocks.Count; i++)
        {
            blocks[i].Broken();
        }
    }

    List<BlockController> GetBlocks(Vector3 direction)
    {
        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(transform.position, direction);

        //Rayの飛ばせる距離
        float distance = direction.magnitude;

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);

        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction, distance);

        //Rayが当たったオブジェクトが存在するか
        return hits.Select(h => h.collider.gameObject.GetComponent<BlockController>())
            .Where(b => b)
            .ToList();
    }

    public void SetView(int hp)
    {
        if (blockType == BlockType.Needle) { return; }
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