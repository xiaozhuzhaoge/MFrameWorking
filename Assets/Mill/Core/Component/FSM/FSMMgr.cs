using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XLua;

/// <summary>
/// 状态及管理器 用于管理状态机逻辑
/// </summary>
public class FSMMgr {

    public FSMMgr() { }
    public FSMMgr(GameObject owner) { this.owner = owner; }

    public GameObject owner;
    private Dictionary<string, FSMBase> fsms = new Dictionary<string, FSMBase>();

    /// <summary>
    /// 状态机容器  
    /// </summary>
    public Dictionary<string, FSMBase> Fsms
    {
        get
        {
            return fsms;
        }

        set
        {
            fsms = value;
        }
    }

    /// <summary>
    /// 根状态
    /// </summary>
    public FSMBase RootState
    {
        get
        {
            return rootState;
        }

        set
        {
            rootState = value;
        }
    }

    /// <summary>
    /// 当前状态
    /// </summary>
    public FSMBase CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }

    /// <summary>
    /// 上一次状态
    /// </summary>
    public FSMBase LastState
    {
        get
        {
            return lastState;
        }

        set
        {
            lastState = value;
        }
    }

    public bool IsContainState(string name) {
        return Fsms.ContainsKey(name);
    }

    private FSMBase rootState;
    private FSMBase currentState;
    private FSMBase lastState;


    /// <summary>
    /// 注册状态
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fsm"></param>
    /// <returns></returns>
    public bool RegisterState(FSMBase fsm) {
        if (IsContainState(fsm.Name))
            return false;
        Fsms.Add(fsm.Name, fsm);
        fsm.ownerMgr = this;
        return true;
    }

    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool RemoveState(string name) {
        if (IsContainState(name)) {
            Fsms.Remove(name);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 查找状态机
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public FSMBase GetState(string name) {
        if (!IsContainState(name))
        {
            return null;
        }
        return Fsms[name];
    }

    /// <summary>
    /// 跳向指定状态
    /// </summary>
    public FSMBase MoveState(string name) {

        if (!IsContainState(name))
        {
            return null;
        }

        lastState = currentState;
        if(currentState != null)
            currentState.OnExit();

        currentState = GetState(name);
      
        currentState.OnEnter();

        return currentState;
    }

    /// <summary>
    /// 设置根节点
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public bool SetRoot(FSMBase root) {
        if (RootState != null)
            return false;
        RootState = root;
        RegisterState(root);
        MoveState(root.Name);
        return true;
    }

    /// <summary>
    /// 获取指定状态机Luatable
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public LuaTable GetLuaTable(string name) {
        if (IsContainState(name))
        {
            return GetState(name).scriptEnv;
        }
        return null;
    }

}
