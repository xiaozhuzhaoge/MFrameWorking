using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadResources : MonoBehaviour {

	/// <summary>
	/// 进度条显示	
	/// </summary>
	public Image progressBar;
	public Text progressContentText;

	void Awake(){
		progressBar = transform.Find ("loadingfornt").GetComponent<Image>();
		progressContentText = transform.Find ("loadingContent").GetComponent<Text> ();
	}

	// Use this for initialization
	void Start () {
		progressBar.fillAmount = 0;
		progressContentText.text = "";
		///注册事件 数据变动自动调取显示变动方法
		ResourceMgr.Instance.OnProgressChange += OnChange;
		///注册事件 数据变动自动调取显示变动方法
		ResourceMgr.Instance.OnProgressContentChange += OnContentChange;
	}
 
	public void OnChange(float value){
		progressBar.fillAmount = value;
	}

	public void OnContentChange(string content){
		progressContentText.text = content;
	}


}
