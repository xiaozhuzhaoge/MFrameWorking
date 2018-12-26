using UnityEngine;
using System.Collections;
using HutongGames;

public abstract class FSMBase{

    public FSMMgr ownerMgr;
    private string name;
    /// <summary>
    /// 状态机命名
    /// </summary>
    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }


    protected PlayMakerFSM fsm {
       get { return ownerMgr.owner.GetComponent<PlayMakerFSM>(); }
    }

    public FSMBase() { }
  
    public FSMBase(params object[] datas) {
        OnDataAnalysis(datas);
    }


    /// <summary>
    /// 用于解析状态机数据
    /// </summary>
    /// <param name="datas"></param>
    public virtual void OnDataAnalysis(params object[] datas) {
        ownerMgr = datas[0] as FSMMgr;
        Name = datas[1] as string;
    }

    public virtual void OnEnter(params object[] datas) { }

    public virtual void OnExit(params object[] datas) { }

    public virtual void OnUpdate() { }

    public virtual void OnFixedUpdate() { }

    public virtual void OnLatedUpdate() { }

    public virtual void MoveState(string stateName)
    {
        ownerMgr.MoveState(stateName);
    }

}
