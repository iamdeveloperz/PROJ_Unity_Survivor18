using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotUI : MonoBehaviour, IDropHandler
{
    private Image _icon;
    private TextMeshProUGUI _quantity;
    private QuickSlotSystem _quickSlotSystem;
    public int index = 0;

    private void Awake()
    {
        _icon = transform.Find("Icon").GetComponent<Image>();
        _quantity = transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        _quickSlotSystem = GameObject.Find("Player").GetComponent<QuickSlotSystem>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Quick Dropped");
        //Inventory.Instance.itemSlots[ItemPreview.instance.sourceIndex].item;
        //quickslotsystem.regist(
        var itemSlot = Inventory.Instance.itemSlots[ItemPreview.instance.sourceIndex];
        _quickSlotSystem.Registe(index, itemSlot.item);
        _icon.sprite = itemSlot.item.icon;
        _quantity.gameObject.SetActive(itemSlot.item.canStack);
        _quantity.text = itemSlot.quantity.ToString();
    }
}
