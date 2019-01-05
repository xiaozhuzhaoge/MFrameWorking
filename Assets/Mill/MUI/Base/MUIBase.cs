using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 系统界面控制基类 注意此类只兼容全局唯一的界面组件
/// </summary>
public abstract class MUIBase : MonoBehaviour, IWindowController, IWindowAniamation
{
	public string uiName;
    /// <summary>
    /// 关闭下面的UI
    /// </summary>
    public bool HideUnderUIs;

    /// <summary>
    /// 下方黑色遮罩
    /// </summary>
    private Image backgroundBoxCom;
    /// <summary>
    /// 是否生成边缘黑框
    /// </summary>
    public bool BlackBox;

    /// <summary>
    /// 边缘区域是否能执行射线检测(可否点击)
    /// </summary>
    public bool CanPassBlackBox;

	/// <summary>
	/// 是否在显示
	/// </summary>
	public bool IsShowed;

	public RectTransform rectTrans;

	/// <summary>
	/// 不遵循弹出规则 勾上代表主界面 不支持弹出功能
	/// </summary>
	public bool NoBackRule;
	public void Awake(){
		rectTrans = transform as RectTransform;
		
		//注册事件到打开和关闭中心
		MessageCenter.Instance.RegisterMessages(uiName + "OpenWindow",gameObject, OpenWindow);
		MessageCenter.Instance.RegisterMessages(uiName + "CloseWindow",gameObject,CloseWindow);
		MessageCenter.Instance.RegisterMessages(uiName + "BackSquence",gameObject,BackSequence);
	}
	/// <summary>
	/// 创建黑色盒子
	/// </summary>
    private void CreateBlackBoxCom()
    {

        /// <summary>
        /// 不能点击下方证明背景接受射线检测
        /// </summary>
        /// <value></value>
        if (!CanPassBlackBox)
        {
            if (backgroundBoxCom == null)
            {
                backgroundBoxCom = new GameObject().AddComponent<Image>();
                backgroundBoxCom.rectTransform.SetParent(transform);
            }
			
			backgroundBoxCom.color = new Color(backgroundBoxCom.color.r,backgroundBoxCom.color.g,backgroundBoxCom.color.b,0.01f);
            backgroundBoxCom.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
            backgroundBoxCom.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);

            if (BlackBox)
            {
			    backgroundBoxCom.color = new Color(backgroundBoxCom.color.r,backgroundBoxCom.color.g,backgroundBoxCom.color.b,0.2f);
                backgroundBoxCom.color = Color.black;
            }
        }
    }


    /// <summary>
    /// 开启界面
    /// </summary>
    /// <param name="data"></param>    
    public virtual void OpenWindow(params object[] data)
    {	
		CreateBlackBoxCom();
        DoOpenAnimation();
		IsShowed = true;
		
		rectTrans.SetAsLastSibling();
		rectTrans.SetParent(GameObject.Find("Canvas").transform,true);
		gameObject.SetActive(true);
    }

    /// <summary>
    /// 关闭时执行的动画效果
    /// </summary>
    public virtual void CloseWindow(params object[] data)
    {
        DoCloseAnimation();
		IsShowed = false;
		rectTrans.SetAsFirstSibling();
		gameObject.SetActive(false);
    }


    /// <summary>
    /// 关闭当前界面 开启下层界面
    /// </summary>
    /// <returns></returns>
    public virtual void BackSequence(params object [] objs)
    {
        CloseWindow();
    }

    /// <summary>
    /// 执行开启动画
    /// </summary>
    public virtual void DoOpenAnimation() { }

    /// <summary>
    /// 执行关闭动画
    /// </summary>
    public virtual void DoCloseAnimation() { }
}
