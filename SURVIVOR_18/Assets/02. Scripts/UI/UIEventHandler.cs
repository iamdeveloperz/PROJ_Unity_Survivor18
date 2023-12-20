using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, 
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, 
    IPointerEnterHandler, IPointerExitHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Events Actions

    /* Pointer Action Buttons */
    public Action<PointerEventData> OnClickEvent;
    public Action<PointerEventData> OnPointerDownEvent; // == Press
    public Action<PointerEventData> OnPointerUpEvent;

    /* Pointer Action Movement */
    public Action<PointerEventData> OnPointerEnterEvent;
    public Action<PointerEventData> OnPointerExitEvent;
    
    /* Drags */
    public Action<PointerEventData> OnBeginDragEvent;
    public Action<PointerEventData> OnDragEvent;
    public Action<PointerEventData> OnEndDragEvent;

    #endregion



    #region Pointer Buttons

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke(eventData);
    }

    #endregion



    #region Pointer Movements

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterEvent?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitEvent?.Invoke(eventData);
    }

    #endregion



    #region Drags

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(eventData);
    }

    #endregion
}
