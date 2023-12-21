using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public Image itemImage;  // 아이템의 이미지
    public Button itemSelectBtn;  // 아이템 선택버튼
    public int index = 0;

    public GameObject quantityTxt;
    public void ClearSlot()
    {
        itemImage.sprite = null;
        quantityTxt.SetActive(false);
        Inventory.Instance.TextClose();
        Inventory.Instance.itemSlots[index] = new ItemSlot();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Inventory.Instance.ItemInfoText(this.index);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) //오른쪽 클릭이면 해당 오브젝트의 아이템을 사용
        {
            Inventory.Instance.UseItem(this.index);
        }
        if (eventData.button == PointerEventData.InputButton.Left) //오른쪽 클릭이면 해당 오브젝트의 아이템을 사용
        {
            Inventory.Instance.ItemInfoText(this.index);
        }
    }

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
