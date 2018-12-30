using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillItemBtn : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public int Index;
    public bool isPress;

    public KeyCode code;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        AttackMenu.Instance.OnButtonClick(Index, isPress);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
        AttackMenu.Instance.OnButtonClick(Index, isPress);
    }

    //public void FixedUpdate()
    //{
    //    if (Input.GetKeyDown(code))
    //    {
    //        isPress = true;
    //        AttackMenu.Instance.OnButtonClick(Index, isPress);
    //    }
    //    if (Input.GetKeyUp(code))
    //    {
    //        isPress = false;
    //        AttackMenu.Instance.OnButtonClick(Index, isPress);
    //    }
    //}
}
