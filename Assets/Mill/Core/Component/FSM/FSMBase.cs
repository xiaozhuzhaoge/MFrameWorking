using UnityEngine;
using System.Collections;
using HutongGames;
using XLua;
using System;

[LuaCallCSharp]
public abstract class FSMBase
{
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

    private LuaTable scriptEnv;
    private Action luaEnter;
    private Action luaUpdate;
    private Action luaExit;
    private Action luaFixedUpdate;
    private Action luaLateUpdate;

    protected PlayMakerFSM fsm
    {
        get { return ownerMgr.owner.GetComponent<PlayMakerFSM>(); }
    }

    public FSMBase() { }

    public FSMBase(params object[] datas)
    {
        OnDataAnalysis(datas);
    }


    /// <summary>
    /// 用于解析状态机数据
    /// </summary>
    /// <param name="datas"></param>
    public virtual void OnDataAnalysis(params object[] datas)
    {

        ownerMgr = datas[0] as FSMMgr;
        Name = datas[1] as string;
        
        TextAsset ta = ResourceMgr.Load<TextAsset>("Luas/"+name+".lua");

        Debug.Log("加载脚本" + ta);
        if (ta == null)
            return;

        scriptEnv = LuaEv.SMachine.NewTable();
        LuaTable meta = LuaEv.SMachine.NewTable();
        meta.Set("__index", LuaEv.SMachine.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);
        LuaEv.SMachine.DoString(ta.text, "FSMBase", scriptEnv);

        scriptEnv.Get("OnEnter", out luaEnter);
        scriptEnv.Get("OnUpdate", out luaUpdate);
        scriptEnv.Get("OnExit", out luaExit);
        scriptEnv.Get("OnFixedUpdate", out luaFixedUpdate);
        scriptEnv.Get("OnLatedUpdate", out luaLateUpdate);
    }

    public virtual void OnEnter(params object[] datas) { if (luaEnter != null) { luaEnter(); } }

    public virtual void OnExit(params object[] datas) { if (luaExit != null) { luaExit(); } }

    public virtual void OnUpdate() { if (luaUpdate != null) { luaUpdate(); } }

    public virtual void OnFixedUpdate() { if (luaFixedUpdate != null) { luaFixedUpdate(); } }

    public virtual void OnLatedUpdate() { if (luaLateUpdate != null) { luaLateUpdate(); } }

    public virtual void MoveState(string stateName)
    {
        ownerMgr.MoveState(stateName);
    }

}
