using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class GridUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static Action<Transform> OnEnter; //显示信息
    public static Action OnExit;

    public static Action<Transform> OnBeginDrag; //拖拽
    public static Action<Transform, Transform> OnEndDrag;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            if (OnEnter != null)
                OnEnter(transform);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            if (OnExit != null)
                OnExit();
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("拖拽开始");
        if (eventData.pointerDrag.tag == "Grid" && transform.childCount != 0)
        {
            if (OnBeginDrag != null)
            {
                OnBeginDrag(transform);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("拖拽结束");
        if (OnEndDrag != null)
        {
            if (eventData.pointerEnter == null)
                OnEndDrag(transform, null);
            else
                OnEndDrag(transform, eventData.pointerEnter.transform);
        }
    }
}
