using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IPointerDownHandler, IEndDragHandler
{

    public Transform lastPapa;
    public Image imge;

    void Awake()
    {
        imge = GetComponent<Image>();
    }

    // Use this for initialization
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform);
        imge.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastResult rr = eventData.pointerCurrentRaycast;
        imge.raycastTarget = true;

        if (rr.gameObject == null)
        {
            ReturnPos();
        }
        else
        {
            UIItem item = rr.gameObject.GetComponent<UIItem>();
            if (item != null)
            {
                SwapPP(item);
            }
            else if (rr.gameObject.tag == "Block")
            {
                if (item != null)
                {
                    SwapPP(item);
                }
                else
                {
                    ChangePP(rr);
                }
            }
            else
            {
                ReturnPos();
            }
        }
    }

    public void SwapPP(UIItem item)
    {
        Transform temp = item.lastPapa;
        item.lastPapa = lastPapa;
        lastPapa = temp;

        transform.SetParent(lastPapa);
        item.transform.SetParent(item.lastPapa);

        transform.localPosition = Vector3.zero;
        item.transform.localPosition = Vector3.zero;
    }

    public void ChangePP(RaycastResult rr)
    {
        transform.SetParent(rr.gameObject.transform);
        transform.localPosition = Vector3.zero;
        lastPapa = transform.parent;
    }

    public void ReturnPos()
    {
        Debug.Log("EndDrag");
        transform.SetParent(lastPapa);
        transform.localPosition = Vector3.zero;
    }
}
