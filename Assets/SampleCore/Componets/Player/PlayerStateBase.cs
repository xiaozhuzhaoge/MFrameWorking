using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class PlayerStateBase : FSMBase
{
    protected GameObject owner;
    protected Animator ani;
    protected CharacterController cc;
    protected string anistateName;

    public string LayerName { get { return fsm.FsmVariables.GetFsmString("AniStateName").Value; } }
    public float CurrentAniLength { get { return fsm.FsmVariables.GetFsmFloat("CurrentAniLength").Value; } }
    public int CurrentLoopCount { get { return fsm.FsmVariables.GetFsmInt("CurrentLoopCount").Value; } }
    public float CurrentLoopProgress { get { return fsm.FsmVariables.GetFsmFloat("CurrentLoopProgress").Value; } }
    public float CurrentNormalizedTime { get { return fsm.FsmVariables.GetFsmFloat("CurrentNormalizedTime").Value; } }

    public PlayerStateBase()
    {
    }

    public PlayerStateBase(params object[] datas) : base(datas)
    {
    }

    public override void OnDataAnalysis(params object[] datas)
    {
        base.OnDataAnalysis(datas);
        owner = ownerMgr.owner;
        ani = owner.GetComponent<Animator>();
        cc = owner.GetComponent<CharacterController>();

    }
}
