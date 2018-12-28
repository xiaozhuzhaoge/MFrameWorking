using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class PlayerStateBase : FSMBase
{
    public GameObject owner;
    public Animator ani;
    public CharacterController cc;
    public string anistateName;

    public string LayerName { get { return fsm.FsmVariables.GetFsmString("AniStateName").Value; } }
    public float CurrentAniLength { get { return fsm.FsmVariables.GetFsmFloat("CurrentAniLength").Value; } }
    public int CurrentLoopCount { get { return fsm.FsmVariables.GetFsmInt("CurrentLoopCount").Value; } }
    public float CurrentLoopProgress { get { return fsm.FsmVariables.GetFsmFloat("CurrentLoopProgress").Value; } }
    public float CurrentNormalizedTime { get { return fsm.FsmVariables.GetFsmFloat("CurrentNormalizedTime").Value; } }

    public PlayerStateBase()
    {
    }

    public PlayerStateBase(params object[] datas)
    {
        OnDataAnalysis(datas);
        owner = ownerMgr.owner;
        ani = owner.GetComponent<Animator>();
        cc = owner.GetComponent<CharacterController>();
    }
}
