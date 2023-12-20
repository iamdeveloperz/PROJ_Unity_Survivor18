using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    public static ItemPreview instance;
    private RectTransform _rectTransform;
    private Image _icon;
    public bool registed;
    public int sourceIndex;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _icon = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void SetIcon(Sprite icon)
    {
        _icon.sprite = icon;
        registed = false;
    }

    public void Init(int sourceIndex)
    {
        if (Inventory.Instance.itemSlots[sourceIndex].item != null)
        {
            var itemSlot = Inventory.Instance.itemSlots[sourceIndex];
            _icon.sprite = itemSlot.item.icon;
            this.sourceIndex = sourceIndex;
        }
    }
}
