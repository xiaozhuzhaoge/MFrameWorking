using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;

[LuaCallCSharp]
public class LuaComponent : MonoBehaviour {

    private Action luaAwake;
    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;

	public string luaScriptName;
    private LuaTable scriptEnv;

    private void Awake() {
        OnDataAnalysis();
    }

	// Use this for initialization
	void Start () {
		if (luaStart != null) { luaStart(); }
	
    }
	
	// Update is called once per frame
	void Update () {
		if (luaUpdate != null) { luaUpdate(); }
	}

    private void OnDestroy() {
        if (luaOnDestroy != null) { luaOnDestroy(); }
    }

	/// <summary>
    /// 用于解析状态机数据
    /// </summary>
    /// <param name="datas"></param>
    public virtual void OnDataAnalysis(params object[] datas)
    {
       
        //Debug.Log("Luas/" + preFixId + name + ".lua");

        TextAsset ta = ResourceMgr.Load<TextAsset>("Luas/" + luaScriptName + ".lua");
  
        Debug.Log("加载本地脚本" + ta);

        if (ta == null)
        {
            ta = ResourceMgr.Load<TextAsset>(luaScriptName + ".lua");
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
        LuaEv.SMachine.DoString(ta.text,luaScriptName, scriptEnv);

        scriptEnv.Get("Start", out luaStart);
        scriptEnv.Get("Update", out luaUpdate);
        scriptEnv.Get("Destroy", out luaOnDestroy);
        scriptEnv.Get("Awake", out luaAwake);

        if (luaAwake != null) { luaAwake(); }
    }
}
