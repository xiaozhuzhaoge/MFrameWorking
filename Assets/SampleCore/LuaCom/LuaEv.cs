using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[CSharpCallLua]
public class LuaEv : MonoSingleton<LuaEv> {

    private static LuaEnv mev;
    /// <summary>
    /// 获取Lua虚拟机 全局唯一
    /// </summary>
    public static LuaEnv SMachine
    {
        set {
            mev = value;
        }
        get
        {
            if (mev == null) mev = new LuaEnv();
            return mev;
        }
    }

    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 

    public void Update()
    {
        LuaGC();
    }

    /// <summary>
    /// LuaGC
    /// </summary>
    public void LuaGC()
    {
        if (mev == null)
            return;

        if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
        {
            mev.Tick();
            LuaBehaviour.lastGCTime = Time.time;
        }
    }

    /// <summary>
    /// 释放环境
    /// </summary>
    public void OnDispose() {
        if(mev != null)
        mev.Dispose();
    }

    private void OnDestroy()
    {
        OnDispose();
    }
}
