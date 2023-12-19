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
    public ItemData item; //������
    public ItemSlot itemSlot; //������
    public Image itemImage;  // �������� �̹���
    public Button itemSelectBtn;  // ������ ���ù�ư

    [SerializeField]
    private GameObject quantityTxt;

    int itemQuantity;
    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(ItemSlot _item)
    {
        if (_item != null)
        {
            item = _item.item;
            itemSlot = _item;
        }
        if (item != null)
        {
            itemImage.sprite = item.icon;
            if (item.canStack)
            {
                itemQuantity = itemSlot.quantity;
                CheckQuantity(itemSlot);
            }
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
        item = null;
        itemImage.sprite = null;
        quantityTxt.SetActive(false);
        for (int i = 0; i < Inventory.Instance.items.Count; i++)
        {
            if (Inventory.Instance.items[i] == itemSlot)
            {
                Inventory.Instance.items.Remove(Inventory.Instance.items[i]);
            }
        }
    }
    public void TextUpdate()
    {
        Inventory.Instance.itemName.text = item.displayName;
        Inventory.Instance.itemInfo.text = item.description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TextUpdate();
        if (eventData.button == PointerEventData.InputButton.Right) //������ Ŭ���̸� �ش� ������Ʈ�� �������� ������
        {
            if (item != null)
            {
                //������ Ÿ�Կ� ���� ����, ���
                //if (item.GetType == )
                //{

                //}
                if (true)
                {
                    // �Һ�
                    itemQuantity--;
                    if (itemQuantity <= 0)
                    {
                        ClearSlot();
                        return;
                    }
                    quantityTxt.GetComponent<TextMeshProUGUI>().text = itemQuantity.ToString();
                }
            }
        }
    }
}
