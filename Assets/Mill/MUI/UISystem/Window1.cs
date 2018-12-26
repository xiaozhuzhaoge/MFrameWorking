using UnityEngine;
using System.Collections;

public class Window1 : UIBase {

	public override void OnInit ()
	{
		base.OnInit ();
		Instance = this;
		name = windowName;
		UIManager.instance.RegisterToCaches (this);
	}


}
