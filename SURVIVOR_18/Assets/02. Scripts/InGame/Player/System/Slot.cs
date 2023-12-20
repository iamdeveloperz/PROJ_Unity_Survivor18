using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour// IPointerClickHandler
{
    public Image itemImage;  // 아이템의 이미지
    public Button itemSelectBtn;  // 아이템 선택버튼
    public int index = 0;

    public GameObject quantityTxt;

    private int itemQuantity;

    public void Consumeitem(int value)
    {
        if (itemQuantity - value >= 0)
        {
            itemQuantity -= value;
            quantityTxt.GetComponent<TextMeshProUGUI>().text = itemQuantity.ToString();
            if (itemQuantity == 0)
            {
                ClearSlot();
            }
        }
        else
        {
            Debug.Log("수량이 부족함");
        }
    }
    public void CheckQuantity(ItemSlot slot)
    {
        if (slot.item.canStack && slot.quantity > 1)
        {
            quantityTxt.SetActive(true);
            quantityTxt.GetComponent<TextMeshProUGUI>().text = slot.quantity.ToString();
            itemQuantity = slot.quantity;
        }
    }
    public void ClearSlot()
    {
        itemImage.sprite = null;
        quantityTxt.SetActive(false);
        Inventory.Instance.TextClose();
        Inventory.Instance.itemSlots[index] = new ItemSlot();
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Inventory.Instance.ItemInfoText(this.index, this);
    //    if (eventData.button == PointerEventData.InputButton.Right) //오른쪽 클릭이면 해당 오브젝트의 아이템을 장착함
    //    {
    //        Inventory.Instance.UseItem(this.index);
    //    }
    //}
    public void SetItemSlot(ItemSlot itemslot)
    {
        if(itemslot.item == null)
        {
            itemImage.sprite = null;
            quantityTxt.SetActive(false);
        }
        else
        {
            itemImage.sprite = itemslot.item.icon;
            quantityTxt.SetActive(itemslot.item.canStack);
            if (itemslot.item.canStack)
            {                
                quantityTxt.GetComponent<TextMeshProUGUI>().text = itemslot.quantity.ToString();
            }
        }
    }
}
