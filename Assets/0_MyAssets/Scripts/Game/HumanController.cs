using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum HumanState
{
    Idle,
    Run,
    Jumping,
}
public class HumanController : MonoBehaviour
{
    [NonSerialized] public Rigidbody rb;
    [SerializeField] ParticleSystem splatPS;
    public ParticleSystem addPS;
    [SerializeField] Animator animator;
    [SerializeField] Collider footCol;

    Collider col;
    PullController pullController;
    //[NonSerialized] public bool isTop;
    public HumanState state { get; set; }
    public Rigidbody GetRigidbody => rb;
    BulletManager bulletManager;
    public BulletManager BulletManager => bulletManager;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        bulletManager = GetComponent<BulletManager>();
        pullController = GetComponent<PullController>();
        state = HumanState.Idle;
    }

    public void OnInstantiate()
    {
        state = HumanState.Idle;
        // isTop = true;
    }

    void Start()
    {
        col.isTrigger = true;
        if (state == HumanState.Run)
        {
            EnableRun();
        }
        else
        {

            //rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void FixedUpdate()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        switch (state)
        {
            case HumanState.Idle:
                break;
            case HumanState.Run:
                break;
            case HumanState.Jumping:
                var groundTfm = GetGround();
                if (groundTfm)
                {
                    state = HumanState.Run;
                    animator.SetTrigger("JumpEnd");
                }
                break;
            default:
                break;
        }
    }

    void LateUpdate()
    {
        if (state == HumanState.Run)
        {
            if (transform.parent)
            {
                var pos = transform.localPosition;
                pos.y = 0;
                pos.z = 0;
                transform.localPosition = pos;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        CheckHuman(other);
        CheckBlock(other);
        CheckGoal(other);
    }

    void CheckGoal(Collider other)
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        if (!other.transform.CompareTag("Goal")) { return; }
        Variables.screenState = ScreenState.Clear;
    }

    public void DragHorizontal(float xSpeed)
    {

    }

    public void EnableRun()
    {
        state = HumanState.Run;
        //col.isTrigger = false;
        rb.isKinematic = true;
        animator.SetTrigger("Run");
        footCol.enabled = false;
    }

    void CheckBlock(Collider other)
    {
        //if (!isTop) { return; }
        var block = other.GetComponent<BlockController>();
        if (block == null) { return; }
        Debug.Log("aaaaaaaaaaaaaaa");
        gameObject.SetActive(false);
        HumansManager.i.humanControllers.Remove(this);
        HumansManager.i.HideHuman();
        var ps = Instantiate(splatPS, Vector3.zero, Quaternion.identity);
        var pos = transform.position;
        pos.y = 1.5f;
        pos.z -= 0.5f;
        ps.transform.position = pos;
        ps.Play();
    }

    void CheckHuman(Collider other)
    {
        if (state == HumanState.Idle) { return; }
        var otherHuman = other.GetComponent<HumanController>();
        if (otherHuman == null) { return; }
        if (otherHuman.state != HumanState.Idle) { return; }
        HumansManager.i.SetHumansList(otherHuman);
        /// GameCanvasManager.i?.ShowAddText(CameraController.i.mainCam, transform.position);
        addPS.Play();
        otherHuman.BulletManager.BulletProperty = bulletManager.BulletProperty;
    }

    public void SetFollower(HumanController follower)
    {
        pullController.follower = follower;
    }

    Transform GetGround()
    {
        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの飛ばせる距離
        float distance = 0.1f;

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);

        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Physics.Raycast(ray, out hit, distance))
        {
            //Rayが当たったオブジェクトが存在するか
            return hit.collider.transform;
        }
        return null;
    }

}
