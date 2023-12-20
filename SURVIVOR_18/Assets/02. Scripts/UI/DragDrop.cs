using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler,IEndDragHandler, IDropHandler
{
    private Slot _slot;
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _slot = GetComponent<Slot>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 localPosition;
            RectTransform rt = _canvas.transform as RectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, mousePosition, _canvas.worldCamera, out localPosition);
            if (ItemPreview.instance.gameObject != null)
            {
                ItemPreview.instance.gameObject.SetActive(true);
                ItemPreview.instance.GetComponent<RectTransform>().localPosition = localPosition;
                ItemPreview.instance.Init(_slot.index);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ItemPreview.instance.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ItemPreview.instance.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        ItemPreview.instance.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Inventory.Instance.SwitchingItemSlot(ItemPreview.instance.sourceIndex, _slot.index);
    }
}
