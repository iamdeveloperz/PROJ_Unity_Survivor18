using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IDropHandler
{
    public ItemData item; //아이템
    public Image itemImage;  // 아이템의 이미지
    public Button itemSelectBtn;  // 아이템 선택버튼
    [SerializeField]
    private GameObject quantityTxt;
    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(ItemData _item)
    {
        item = _item;
        if (item != null)
        {
            itemImage.sprite = item.itemImage;
        }
    }

    // 해당 슬롯 하나 삭제
    private void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
    }
    public void CheckQuantity(ItemSlot slot)
    {
        Debug.Log(slot.item.name);
        if (slot.item.canStack && slot.quantity > 1)
        {
            quantityTxt.SetActive(true);
            quantityTxt.GetComponent<TextMeshProUGUI>().text = slot.quantity.ToString();
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        // 드롭한 아이템이 있다면 해당 아이템을 현재 슬롯에 추가합니다.
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        if (itemDragHandler != null)
        {
            itemDragHandler.DropItemToSlot(this);
        }
    }
}
