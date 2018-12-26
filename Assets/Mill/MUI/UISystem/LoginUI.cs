using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginUI : UIBase {

	public Button btn;

	public override void OnInit ()
	{
		base.OnInit ();
		btn.onClick.AddListener (delegate {
			OpenOtherWindow<Window1>("Window1");
		});
	}

	[ContextMenu("Window1")]
	public void OpenWindow1(){
		OpenOtherWindow<Window1>("Window1");
	}
	[ContextMenu("Window2")]
	public void OpenWindow2(){
		OpenOtherWindow<Window2>("Window2");
	}

	void OnGUI(){
		if (GUILayout.Button ("Open1")) {
			OpenOtherWindow<Window1>("Window1");

		}
		if (GUILayout.Button ("Open2")) {
			OpenOtherWindow<Window2>("Window2");
		}

		if (GUILayout.Button ("Close1")) {
			CloseOtherWindow<Window1>("Window1");

		}
		if (GUILayout.Button ("Close2")) {
			CloseOtherWindow<Window2>("Window2");
		}

		if (GUILayout.Button ("Back")) {
			Back ();
		}
	}
}
