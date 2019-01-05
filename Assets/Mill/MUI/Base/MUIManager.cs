using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于管理界面逻辑的管理器
/// </summary>
public class MUIManager : Singleton<MUIManager>, IRecovery
{

    /// <summary>
    /// 所有UI贮存缓存
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <typeparam name="MUIBase"></typeparam>
    /// <returns></returns>
    private Dictionary<string, MUIBase> uis = new Dictionary<string, MUIBase>();
    public Dictionary<string, MUIBase> UIs
    {
        get { return uis; }
        set { uis = value; }
    }

    /// <summary>
    /// 序列逻辑
    /// </summary>
    /// <typeparam name="MUIBase"></typeparam>
    /// <returns></returns>
    private StackList<MUIBase> squeneces = new StackList<MUIBase>();


    public void OpenUI(string uiName,params object[] objs)
    {
        /// <summary>
        /// 创建UI
        /// </summary>
        /// <returns></returns>
        if (!uis.ContainsKey(uiName))
        {
            var ui = CreateUI(uiName).GetComponent<MUIBase>();
            UIs.Add(ui.uiName, ui);
        }

        /// <summary>
        /// 放置顺序
        /// </summary>
        /// <value></value>
        if (uis.ContainsKey(uiName))
        {
            if (!uis[uiName].NoBackRule)
                squeneces.Push(uis[uiName]);
            MessageCenter.Instance.FrenchMessages(uis[uiName].gameObject, uiName + "OpenWindow",objs);

        }

    }

    public void CloseUI(string uiName,params object[] objs)
    {
        if (uis.ContainsKey(uiName))
        {
            if (squeneces.Count > 0 && !uis[uiName].NoBackRule)
            {
                squeneces.Pop(uis[uiName]);
            }

            MessageCenter.Instance.FrenchMessages(uis[uiName].gameObject, uiName + "CloseWindow",objs);
        }

    }

    /// <summary>
    /// 创建UI物体
    /// </summary>
    /// <param name="uiName"></param>
    /// <returns></returns>
    public GameObject CreateUI(string uiName)
    {
        return ResourceMgr.CreateObj("GUIs/" + uiName);
    }

    public void CloseTop()
    {
        if (squeneces.Count > 0)
        {
            var data = squeneces.Pop();
            if (data != null)
            {
                CloseUI(data.uiName);
                ///如果有用动画需要等待时间。。。。 这里还不完善
                if (squeneces.Count > 0)
                    OpenUI(squeneces.Peek().uiName);
            }

        }
    }
    /// <summary>
    /// 回收释放AB包资源
    /// </summary>
    public void Recovery()
    {
        foreach (var data in UIs)
        {
            GameObject.Destroy(data.Value.gameObject);
        }
        UIs.Clear();
        squeneces.Clear();
    }
}

/// <summary>
/// 使用链表制作栈的操作方式
/// </summary>
/// <typeparam name="T"></typeparam>
public class StackList<T>
{

    private List<T> caches;

    public StackList()
    {
        caches = new List<T>();
    }

    public void Push(T obj)
    {
        if (caches.Contains(obj))
            caches.Remove(obj);
        caches.Add(obj);
    }

    public T Pop()
    {
        var data = caches[caches.Count - 1];
        caches.Remove(data);
        return data;
    }

    public T Pop(T obj)
    {
        var index = caches.IndexOf(obj);
        if (index >= 0)
        {
            var data = caches[index];
            caches.Remove(obj);
            return data;
        }
        else
        {
            return default(T);
        }

    }

    public T Peek()
    {
        return caches[caches.Count - 1];
    }

    public int GetIndex(T obj)
    {
        return caches.IndexOf(obj);
    }
    public bool Contains(T obj)
    {
        return caches.Contains(obj);
    }
    public void Clear()
    {
        caches.Clear();
    }

    public int Count
    {
        get { return caches.Count; }
    }
}