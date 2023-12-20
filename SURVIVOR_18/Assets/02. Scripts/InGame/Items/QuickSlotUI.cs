using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlotUI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    private Image _icon;
    private TextMeshProUGUI _quantity;
    private QuickSlotSystem _quickSlotSystem;
    public int index = 0;
    private int _targetIndex = -1;

    private void Awake()
    {
        _icon = transform.Find("Icon").GetComponent<Image>();
        _quantity = transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        _quickSlotSystem = GameObject.Find("Player").GetComponent<QuickSlotSystem>();
        _icon.sprite = null;
        _quantity.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Quick Dropped");
        _targetIndex = ItemPreview.instance.sourceIndex;
        var itemSlot = Inventory.Instance.itemSlots[_targetIndex];
        if (Inventory.Instance.itemSlots[_targetIndex].item is RegistableItemData)
        {
            itemSlot.locked = true;
            //_quickSlotSystem.Registe(index, itemSlot.item);
            _quickSlotSystem.Registe(index, itemSlot.item, ItemPreview.instance.sourceIndex);

            _icon.sprite = itemSlot.item.icon;
            _quantity.gameObject.SetActive(itemSlot.item.canStack);
            _quantity.text = itemSlot.quantity.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) //오른쪽 클릭이면 해당 오브젝트의 아이템을 사용
        {
            Inventory.Instance.itemSlots[_targetIndex].locked = false;
            _icon.sprite = null;
            _quantity.gameObject.SetActive(false);
            _quickSlotSystem.UnRegiste(index);
        }
    }

    public void Clear()
    {
        _icon.sprite = null;
        _quantity.gameObject.SetActive(false);
    }
}
