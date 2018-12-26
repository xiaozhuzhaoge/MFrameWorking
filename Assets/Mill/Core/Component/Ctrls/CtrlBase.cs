using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlBase : MonoBehaviour {

	private string ctrName;

    public string Name
    {
        get
        {
            return ctrName;
        }

        set
        {
            ctrName = value;
        }
    }
}
