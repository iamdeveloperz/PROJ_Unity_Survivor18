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
    public ItemData item; //������
    public Image itemImage;  // �������� �̹���
    public Button itemSelectBtn;  // ������ ���ù�ư
    [SerializeField]
    private GameObject quantityTxt;
    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(ItemData _item)
    {
        item = _item;
        if (item != null)
        {
            itemImage.sprite = item.itemImage;
        }
    }

    // �ش� ���� �ϳ� ����
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
        // ����� �������� �ִٸ� �ش� �������� ���� ���Կ� �߰��մϴ�.
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        if (itemDragHandler != null)
        {
            itemDragHandler.DropItemToSlot(this);
        }
    }
}
