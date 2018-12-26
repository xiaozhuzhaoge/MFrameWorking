using UnityEngine;
using System.Collections;

public abstract class UIBase : MonoBehaviour {

	public Animator ani;

	public string windowName;

	[HideInInspector]
	public static UIBase Instance;
	[HideInInspector]
	public GameObject m_target;
 
	private bool isShowed;

	public bool IsShowed{
		set { isShowed = value;}
		get { return isShowed;}
	}
		
	public bool NobackBtn;

	public virtual void OnInit(){
		
	}

	public virtual void Awake(){
		OnInit ();
		Instance = this;
	}


	public virtual void Back(){
		UIManager.instance.BackUI ();
	}

 
	public virtual void OpenWindow(params object[] values){
		IsShowed = true;
		gameObject.SetActive (true);
		transform.SetAsLastSibling ();
		if (ani != null) {
			ani.SetBool ("IsShow",IsShowed);
		}
	}
	public virtual void CloseWindow(){
		IsShowed = false;
		transform.SetAsFirstSibling ();
		gameObject.SetActive (false);
		if (ani != null) {
			ani.SetBool ("IsShow",IsShowed);
		}
	}

	public virtual void CloseOtherWindow<T>(string windowName,params object[] values) where T:UIBase{
		UIManager.instance.CloseUI<T> (windowName,values);
	}

	public virtual void OpenOtherWindow<T>(string windowName,params object[] values) where T: UIBase{
		UIManager.instance.ShowUI<T> (windowName,values);
	}
}



