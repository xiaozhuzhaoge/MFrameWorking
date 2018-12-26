using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour {

    public FSMMgr smm;
    public float SeekRange;
    public float AttackRange;
    public float MoveSpeed;
    public float RotationSpeed;
    public Vector3 orignalPos;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (smm.CurrentState != null)
            smm.CurrentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        if (smm.CurrentState != null)
            smm.CurrentState.OnFixedUpdate();
    }

    public virtual void Init()
    {
    }
}
