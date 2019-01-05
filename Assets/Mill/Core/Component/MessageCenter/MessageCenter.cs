using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnMessageEventHandler(params object[] objects);

public class MessageCenter : Singleton<MessageCenter>,IRecovery
{
    public MessageCenter()
    {
    }
    public MessageCenter(Dictionary<string, Dictionary<GameObject, OnMessageEventHandler>> centerPool)
    {
        this.CenterPool = centerPool;
    }

    Dictionary<string, Dictionary<GameObject, OnMessageEventHandler>> centerPool = new Dictionary<string, Dictionary<GameObject, OnMessageEventHandler>>();

    /// <summary>
    /// 消息池
    /// </summary>
    public Dictionary<string, Dictionary<GameObject, OnMessageEventHandler>> CenterPool
    {
        get
        {
            return centerPool;
        }
        set
        {
            centerPool = value;
        }
    }

    /// <summary>
    /// 回收释放资源 清除所有注册消息
    /// </summary>
    public void Recovery(){
        foreach(var data in centerPool){
            RemoveMessageHandler(data.Key);
        }
        centerPool.Clear();
    }

    /// <summary>
    /// 查找消息数据
    /// </summary>
    /// <param name="MessageHeader"></param>
    /// <returns></returns>
    public Dictionary<GameObject,OnMessageEventHandler> FindMessage(string MessageHeader) {
        if (CenterPool.ContainsKey(MessageHeader))
            return CenterPool[MessageHeader];
        else
            return null;
    }

    /// <summary>
    /// 注册方法
    /// </summary>
    /// <param name="Message">消息头</param>
    /// <param name="go">消息接收物体</param>
    /// <param name="eventHandler">事件回调</param>
    public void RegisterMessages(string Message,GameObject go, OnMessageEventHandler eventHandler) {

        if(eventHandler == null)
        {
            Debug.LogError("请注册一个方法" + go + eventHandler);
            return;
        }

        if (!CenterPool.ContainsKey(Message))
        {
            centerPool.Add(Message, new Dictionary<GameObject, OnMessageEventHandler>());
        }


        if (!centerPool[Message].ContainsKey(go))
        {
            centerPool[Message].Add(go, eventHandler);
        }
        else
        {
            Debug.Log("已经注册了方法");
        }

    }
    
    /// <summary>
    /// 清除指定消息队列
    /// </summary>
    /// <param name="Message"></param>
    public void RemoveMessageHandler(string Message){
        centerPool[Message].Clear();
    }

    /// <summary>
    /// 清除指定对象的消息
    /// </summary>
    /// <param name="target"></param>
    /// <param name="Message"></param>
    public void RemoveMessageHanlder(GameObject target,string Message){
        centerPool[Message].Remove(target);
    }

    /// <summary>
    /// 调用指定物体的消息回调
    /// </summary>
    /// <param name="target"></param>
    /// <param name="Message"></param>
    public void FrenchMessages(GameObject target,string Message,params object[] objects) {
        Dictionary<GameObject, OnMessageEventHandler> messages = FindMessage(Message);
        if (messages.ContainsKey(target))
        {
            messages[target](objects);
        }
    }

    /// <summary>
    /// 广播消息
    /// </summary>
    /// <param name="Message">消息头</param>
    /// <param name="objects">消息数据</param>
    public void BoardCastMessage(string Message, params object[] objects) {
        Dictionary<GameObject, OnMessageEventHandler> messages = FindMessage(Message);
        if(messages != null)
        {
            foreach(var data in messages.Values)
            {
                data(objects);
            }
        }
    }
}
