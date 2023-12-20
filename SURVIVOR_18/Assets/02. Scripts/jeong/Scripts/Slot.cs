using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;  // 아이템의 이미지
    public Button itemSelectBtn;  // 아이템 선택버튼
    public int index = 0;

    [SerializeField]
    private GameObject quantityTxt;

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
        //Inventory.Instance.TextClose();
    }

    private void PlusStatPlayer()
    {
        // Inventory 에서 I아이템 사용을 구현
        // 매개변수는 index 값을 사용
        // slot UI 는 각각 index 값을 가진다.
        // 해당 slot 이 눌리면 Inventory.Instance.UseItem(index) 이런 형식이 될 것 같습니다.

        //for (int i = 0; i < item.consumables.Length; i++)
        //{
        //    switch (item.consumables[i].type)
        //    {
        //        case ConsumableType.Moisture:
        //            Inventory.Instance.playerStatHandler.Eat(item.consumables[i].value);
        //            break;
        //        case ConsumableType.Hunger:
        //            Inventory.Instance.playerStatHandler.Drink(item.consumables[i].value);
        //            break;
        //    }
        //}
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Inventory.Instance.curSlot = this;
        //if (item != null && item.type == ItemType.useItem)
        //{
        //    Inventory.Instance.TextClose();
        //    Inventory.Instance.itemText(item.displayName, item.description, item.type, item.consumables[0].value, item.consumables[1].value);
        //}
        //else if(item != null)
        //{
        //    Inventory.Instance.TextClose();
        //    Inventory.Instance.itemText(item.displayName, item.description);
        //}
        //if (eventData.button == PointerEventData.InputButton.Right) //오른쪽 클릭이면 해당 오브젝트의 아이템을 장착함
        //{
        //if (item != null)
        //{
        //    if (item.type == ItemType.useItem)
        //    {
        //        // 소비
        //        itemQuantity--;
        //        if (itemQuantity <= 0)
        //        {
        //            PlusStatPlayer();
        //            ClearSlot();
        //            Inventory.Instance.TextClose();
        //            return;
        //        }
        //        quantityTxt.GetComponent<TextMeshProUGUI>().text = itemQuantity.ToString();
        //    }
        //}
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
