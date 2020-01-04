using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class GridUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region 鼠标Enter&&Exit事件
    public static Action<Transform> OnEnter;
    public static Action OnExit;
    /// <summary>
    /// 鼠标 进入 时触发该方法
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            if (OnEnter != null)
                OnEnter(transform);
        }
    }
    /// <summary>
    /// 鼠标 移出 时触发该方法
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            if (OnExit != null)
                OnExit();
        }
    }
    #endregion

    #region 拖拽物品事件
    public static Action<Transform> OnLeftBeginDrag;
    public static Action<Transform, Transform> OnLeftEndDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnLeftBeginDrag != null)
                OnLeftBeginDrag(transform);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnLeftEndDrag != null)
            {
                if(eventData.pointerEnter == null)
                    OnLeftEndDrag(transform, null);
                else
                    OnLeftEndDrag(transform, eventData.pointerEnter.transform);
            }
        }
    }
    #endregion
}
