using UnityEngine;
using System.Collections;
using HutongGames;
using XLua;
using System;

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

    private int preFixId;

    private LuaTable scriptEnv;
    private Action luaEnter;
    private Action luaUpdate;
    private Action luaExit;
    private Action luaFixedUpdate;
    private Action luaLateUpdate;

    private Action<Collider> luaTriggerEnter;
    private Action<Collider> luaTriggerStay;
    private Action<Collider> luaTriggerExit;
    private Action<Collision> luaCollisionEnter;
    private Action<Collision> luaCollisionStay;
    private Action<Collision> luaCollisionExit;

    private Action luaBecameInvisible;
    private Action luaBecameVisible;

    private Action luaTransformChildrenChanged;
    private Action luaTransformParentChanged;
    private Action<bool> luaApplicationPause;
    private Action luaDestroy;

    private Action luaEnable;
    private Action luaDisable;


    protected PlayMakerFSM fsm
    {
        get { return ownerMgr.owner.GetComponent<PlayMakerFSM>(); }
    }

    /// <summary>
    /// 前缀ID 和技能组Id相对
    /// </summary>
    public int PreFixId
    {
        get
        {
            return preFixId;
        }

        set
        {
            preFixId = value;
        }
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
        //Debug.Log("长度？" + datas.Length);
        ownerMgr = datas[0] as FSMMgr;
        preFixId = Convert.ToInt32(datas[1]);
        Name = datas[2] as string;

        //Debug.Log("Luas/" + preFixId + name + ".lua");

        TextAsset ta = ResourceMgr.Load<TextAsset>("Luas/" + preFixId + name + ".lua");

        //Debug.Log("加载本地脚本" + ta);

        if (ta == null)
        {
            ta = ResourceMgr.Load<TextAsset>(preFixId + name + ".lua");
            //Debug.Log("读取AB包中脚本" + name);
        }

        if(ta == null)
            return;

        scriptEnv = LuaEv.SMachine.NewTable();
        LuaTable meta = LuaEv.SMachine.NewTable();
        meta.Set("__index", LuaEv.SMachine.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);
        LuaEv.SMachine.DoString(ta.text, preFixId + name, scriptEnv);

        scriptEnv.Get("OnEnter", out luaEnter);
        scriptEnv.Get("OnUpdate", out luaUpdate);
        scriptEnv.Get("OnExit", out luaExit);
        scriptEnv.Get("OnFixedUpdate", out luaFixedUpdate);
        scriptEnv.Get("OnLatedUpdate", out luaLateUpdate);
        scriptEnv.Get("OnTriggerEnter", out luaTriggerEnter);
        scriptEnv.Get("OnTriggerStay", out luaTriggerStay);
        scriptEnv.Get("OnTriggerExit", out luaTriggerExit);
        scriptEnv.Get("OnCollisionEnter", out luaCollisionEnter);
        scriptEnv.Get("OnCollisionStay", out luaCollisionStay);
        scriptEnv.Get("OnCollisionExit", out luaCollisionExit);
        scriptEnv.Get("OnBecameInvisible", out luaBecameInvisible);
        scriptEnv.Get("OnBecameVisible", out luaBecameVisible);

        scriptEnv.Get("OnTransformChildrenChanged", out luaTransformChildrenChanged);
        scriptEnv.Get("OnTransformParentChanged", out luaTransformParentChanged);
        scriptEnv.Get("OnApplicationPause", out luaApplicationPause);
        scriptEnv.Get("OnDestroy", out luaDestroy);
        scriptEnv.Get("OnEnable", out luaEnable);
        scriptEnv.Get("OnDisable", out luaDisable);
    }

    public virtual void OnEnter(params object[] datas) { if (luaEnter != null) { luaEnter(); } }

    public virtual void OnExit(params object[] datas) { if (luaExit != null) { luaExit(); } }

    public virtual void OnUpdate() { if (luaUpdate != null) { luaUpdate(); } }

    public virtual void OnFixedUpdate() { if (luaFixedUpdate != null) { luaFixedUpdate(); } }

    public virtual void OnLatedUpdate() { if (luaLateUpdate != null) { luaLateUpdate(); } }

    public virtual void OnTriggerEnter(Collider other) { if (luaTriggerEnter != null) { luaTriggerEnter(other); } }

    public virtual void OnTriggerStay(Collider other)
    {
        if (luaTriggerStay != null) { luaTriggerStay(other); }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        if (luaTriggerExit != null) { luaTriggerExit(other); }
    }

    public virtual void OnBecameInvisible()
    {
        if (luaBecameInvisible != null) { luaBecameInvisible(); }
    }

    public virtual void OnBecameVisible()
    {
        if (luaBecameVisible != null) { luaBecameVisible(); }
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        if (luaCollisionEnter != null) { luaCollisionEnter(other); }
    }
    public virtual void OnCollisionStay(Collision other)
    {
        if (luaCollisionStay != null) { luaCollisionStay(other); }
    }
    public virtual void OnCollisionExit(Collision other)
    {
        if (luaCollisionExit != null) { luaCollisionExit(other); }
    }

    public virtual void OnDestroy()
    {
        if (luaDestroy != null) { luaDestroy(); }
    }

    public virtual void OnTransformChildrenChanged()
    {
        if (luaTransformChildrenChanged != null) { luaTransformChildrenChanged(); }
    }

    public virtual void OnTransformParentChanged()
    {
        if (luaTransformParentChanged != null) { luaTransformParentChanged(); }
    }

    public virtual void OnApplicationPause(bool pauseStatus)
    {
        if (luaApplicationPause != null) { luaApplicationPause(pauseStatus); }
    }

    public virtual void OnEnable()
    {
        if (luaEnable != null) { luaEnable(); }
    }

    public virtual void OnDisable()
    {
        if (luaDisable != null) { luaDisable(); }
    }

    public virtual void MoveState(string stateName)
    {
        ownerMgr.MoveState(stateName);
    }

}
