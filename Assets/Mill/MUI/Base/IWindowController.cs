using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWindowController
{
    /// <summary>
    /// 弹出显示栈 关闭当前页面
    /// </summary>
    /// <returns></returns>
    void BackSequence(params object [] objs);
}  
