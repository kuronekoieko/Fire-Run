using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UniRx;
public class HumansManager : MonoBehaviour
{
    [SerializeField] HumanController humanPrefab;
    List<HumanController> humanControllers;
    public static HumansManager i;
    Vector3 tapPos;
    [NonSerialized] public float distance = 0.8f;
    [SerializeField] Joystick joystick;
    public Vector3 firstHumanPos
    {
        get
        {
            if (humanControllers.Count == 0) { return _firstHumanPos; }
            _firstHumanPos = humanControllers[0].transform.position;
            return _firstHumanPos;
        }
    }
    private Vector3 _firstHumanPos;
    void Awake()
    {
        i = this;
        humanControllers = new List<HumanController>();
        humanControllers.Add(Instantiate(humanPrefab, Vector3.zero, Quaternion.identity));
        humanControllers[0].OnInstantiate();
        Application.targetFrameRate = 60;
        this.ObserveEveryValueChanged(humanCount => humanControllers.Count)
            .Subscribe(humanCount => Variables.humanCount = humanCount)
            .AddTo(this.gameObject);
    }

    void Start()
    {
        //humanControllers[0].EnableRun();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tapPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 drag = Input.mousePosition - tapPos;
            tapPos = Input.mousePosition;
            SetHorizontalVel(xSpeed: drag.x);
        }


        if (Input.GetMouseButtonUp(0))
        {
            //SetHorizontalVel(xSpeed: 0);
            //Jump();
            SetHorizontalVel(xSpeed: 0);
        }

        // SetHorizontalVel(xSpeed: joystick.Horizontal * 10);

    }

    void FixedUpdate()
    {
    }


    void SetHorizontalVel(float xSpeed)
    {
        if (humanControllers.Count == 0) { return; }
        float limit = 30;
        xSpeed = Mathf.Clamp(xSpeed, -limit, limit);
        humanControllers[0].DragHorizontal(xSpeed);
    }

    void Jump()
    {
        if (humanControllers.Count == 0) { return; }
        humanControllers[0].Jump();
    }

    public void SetHumansList(HumanController human)
    {
        var lastHuman = humanControllers[humanControllers.Count - 1];
        var pos = lastHuman.transform.position;
        pos.x -= distance;
        human.transform.position = pos;
        human.EnableRun();
        human.gameObject.layer = LayerMask.NameToLayer("Follower");
        lastHuman.SetFollower(human);
        humanControllers.Add(human);
    }

    public void HideHuman()
    {
        humanControllers[0].isTop = false;
        humanControllers[0].gameObject.SetActive(false);
        humanControllers.RemoveAt(0);
        if (humanControllers.Count == 0)
        {
            Variables.screenState = ScreenState.Failed;
            return;
        }
        humanControllers[0].isTop = true;
        humanControllers[0].gameObject.layer = LayerMask.NameToLayer("Default");
    }

}
