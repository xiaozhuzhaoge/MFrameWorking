using UnityEngine;
using System.Collections;
using XLua;

public class MakeHotFix : MonoSingleton<MakeHotFix> {

    public string hotFixName;

    protected override void Initialize()
    {
        DontDestroyOnLoad(this);
    }
	/// <summary>
	/// 打热更补丁	
	/// </summary>
	public void MakeHotFixFuc(){
		TextAsset asset = ResourceMgr.Load<TextAsset> (hotFixName);
		if (asset == null) {
            if (hotFixName == "")
                Debug.Log("不存在HOTFIX更新文件");
            else
			    Debug.LogError ("你的热更补丁资源包有问题" + hotFixName);
			return;
		}
		LuaEv.SMachine.DoString (asset.text);
	}
 
}
