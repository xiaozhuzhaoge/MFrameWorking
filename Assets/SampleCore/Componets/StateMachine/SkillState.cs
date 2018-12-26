using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillState : PlayerStateBase
{
    DoEvetHandler OnUpdateCallback;
    public SkillState() { }

    public SkillState(params object[] datas) : base(datas) { }

    public override void OnDataAnalysis(params object[] datas)
    {
        base.OnDataAnalysis(datas);
        OnUpdateCallback = datas[2] as DoEvetHandler;
    }

    public override void OnEnter(params object[] datas)
    {
        ani.SetFloat("speed", 0);
    }

    public override void OnUpdate()
    {
        if(OnUpdateCallback != null)
        {
            OnUpdateCallback(ownerMgr.CurrentState.Name);
        }   
    }

}
