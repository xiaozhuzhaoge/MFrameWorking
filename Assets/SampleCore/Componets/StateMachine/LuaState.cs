using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;

public class LuaState : PlayerStateBase
{
    public LuaState() { }

    public LuaState(params object[] datas): base(datas) { }

}
