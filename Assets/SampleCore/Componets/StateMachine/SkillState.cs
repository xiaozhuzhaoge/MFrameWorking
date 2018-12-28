using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;

[LuaCallCSharp]
public class SkillState : PlayerStateBase
{
    public CharacterController ccc;
    public SkillState() { }

    public SkillState(params object[] datas) : base(datas) { ccc = cc;}

    public override void OnDataAnalysis(params object[] datas)
    {
        base.OnDataAnalysis(datas);
    }
 
}
