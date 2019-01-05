using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpwaner : StateController
{
    private void Awake()
    {

    }

    [ContextMenu("Open1")]
    public void TestWindow()
    {
        MUIManager.Instance.OpenUI("1");
    }
    [ContextMenu("Open2")]
    public void TestWindow2()
    {
        MUIManager.Instance.OpenUI("2");
    }
    [ContextMenu("Close1")]
    public void TestCloseWindow()
    {
        MUIManager.Instance.CloseUI("1");
    }
    [ContextMenu("Close2")]
    public void TestCloseWindow2()
    {
        MUIManager.Instance.CloseUI("2");
    }
[ContextMenu("Back")]
    public void Back()
    {
        MUIManager.Instance.CloseTop();
    }
}
