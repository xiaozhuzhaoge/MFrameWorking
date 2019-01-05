using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWindowAniamation
{
    /// <summary>
    /// 开启时执行的动画效果
    /// </summary>
    void DoOpenAnimation();
    /// <summary>
    /// 关闭时执行的动画效果
    /// </summary>
    void DoCloseAnimation();
}  
