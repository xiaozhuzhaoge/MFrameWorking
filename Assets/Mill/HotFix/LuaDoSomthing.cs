using UnityEngine;
using System.Collections;
using XLua;

public class LuaDoSomthing : MonoBehaviour {

	public TextAsset asset;
	public LuaEnv lv;

	void Awake(){
		lv = new LuaEnv ();
	}

	// Use this for initialization
	void Start () {

		Object o = ResourceMgr.Load("test1.lua");
		TextAsset asset = o as TextAsset;
		lv.DoString (asset.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestory(){
		lv.Dispose ();
	}
}
