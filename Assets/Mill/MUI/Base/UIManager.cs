using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	private static UIManager ins;
	public static UIManager instance {
		get { 
			if (ins == null){
				ins = GameObject.Find("Canvas").GetComponent<UIManager>();
			}
			return ins;
		}
		set { ins = value;}
	}
	public static Dictionary<string,UIBase> windows = new Dictionary<string, UIBase>();
	public void Awake(){
		instance = this;
		 
	}

	public void RegisterToCaches(UIBase baseUI){
		Debug.Log (baseUI.windowName);
		if (!windows.ContainsKey (baseUI.windowName)) {
			windows.Add(baseUI.windowName,baseUI);
		}
	}
	public void RemoveFromCaches(UIBase baseUI){
		if (windows.ContainsKey (baseUI.windowName)) {
			windows.Remove (baseUI.windowName);
		}
	}
	 
	public UIBase GetFromCaches(string name){
		if (windows.ContainsKey (name)) {
			return windows [name];
		}
		return null;
	}

	public void ShowUI<T>(string name, params object[] values) where T:UIBase {
		StartCoroutine(DoShowUI<T>(name));
	}

	public IEnumerator DoShowUI<T>(string name, params object[] values) where T:UIBase 
	{
		T window = GetFromCaches(name) as T;

		if (window == null) {
			Debug.Log (name);
			GameObject go = GameObject.Instantiate(Resources.Load<GameObject> ("GUI/" + name));
			(go.transform as RectTransform).SetParent (transform,false);
			yield return new WaitForEndOfFrame ();
			go.GetComponent<T>().OpenWindow ();
		} else {
			window.OpenWindow ();
		}
	}

	 
	public void CloseUI<T>(string name, params object[] values) where T:UIBase{
		T window = GetFromCaches(name) as T;

		if (window != null) {
			window.CloseWindow ();
		}
	}

	public void BackUI(){
		UIBase baseUI = transform.GetChild(transform.childCount - 1).GetComponent<UIBase> ();
		if(!baseUI.NobackBtn)
			baseUI.CloseWindow();
	}

}
